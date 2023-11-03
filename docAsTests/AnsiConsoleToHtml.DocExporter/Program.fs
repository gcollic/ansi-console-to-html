// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO
open Scriban
open Scriban.Runtime

Environment.GetCommandLineArgs()
|> Array.iteri (fun index arg -> printfn $"arg[{index}] = {arg}")

let rootFolder = Directory.GetCurrentDirectory()
let toRelative s = Path.GetRelativePath(rootFolder, s)
let docFolder = Path.Combine(rootFolder, "doc")

docFolder
|> Directory.GetFiles
|> Array.filter (fun file -> not (file.EndsWith(".gitignore")))
|> Array.iter (fun file ->
    printfn $"Deleting '{toRelative file}'"
    File.Delete file)

Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "assets")
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(docFolder, fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{toRelative file}'"
    printfn $"    to '{toRelative targetPath}'")

let mainLayout =
    Path.Combine(
        rootFolder,
        "docAsTests",
        "AnsiConsoleToHtml.DocExporter",
        "templates",
        "mainLayout.html"
    )
    |> File.ReadAllText
    |> Template.Parse

let parts =
    Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
    |> Directory.GetFiles
    |> Array.map (fun file ->
        Deserializer.parseDocWithOptionalYamlFrontMatter file (File.ReadAllText file))


parts
|> Array.filter (fun p -> p.Metadata.IsSome)
|> Array.iter (fun docPart ->
    let context = new TemplateContext()
    let so = new ScriptObject()

    so.Import(
        {|
            title = "title"
            description = "description"
            homeUrl = "homeurl"
            repoRoot = "repo"
            mainContent = docPart.Content
            slug = docPart.Slug.asString
        |}
    )

    context.PushGlobal(so)

    let templatedContent = mainLayout.Render(context)

    let fileName = Path.Combine(docFolder, docPart.Slug.asString + ".html")
    File.WriteAllText(fileName, templatedContent)
    printfn $"Created '{toRelative fileName}'")

exit 0
