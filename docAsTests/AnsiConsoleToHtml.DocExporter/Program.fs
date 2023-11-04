// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO
open Scriban
open Scriban.Runtime

Environment.GetCommandLineArgs()
|> Array.iteri (fun index arg -> printfn $"arg[{index}] = {arg}")

let rootFolder = Directory.GetCurrentDirectory()
let toRelative path = Path.GetRelativePath(rootFolder, path)
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
        "mainLayout.sbnhtml"
    )
    |> File.ReadAllText
    |> Template.Parse

let allParts =
    Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
    |> Directory.GetFiles
    |> Array.map (fun file ->
        Deserializer.parseDocWithOptionalYamlFrontMatter file (File.ReadAllText file))

let pages = allParts |> Array.filter (fun p -> p.Metadata.IsSome)

let slugToFile (slug: DocPart.Slug) = slug.asString + ".html"

type TocItem = { Label: string; Slug: DocPart.Slug }
type TocGroup = { Name: string; Items: TocItem array }

let toc =
    pages
    |> Seq.ofArray
    |> Seq.map (fun page ->
        match page with
        | { Metadata = Some { Toc = Some t } } -> Some(page.Slug, t)
        | _ -> None)
    |> Seq.choose id
    |> Seq.groupBy (fun (_, toc) -> toc.Parent)
    |> Seq.map (fun (key, values) -> {
        Name = key
        Items =
            values
            |> Seq.sortBy (fun (_, toc) -> toc.Order)
            |> Seq.map (fun (slug, toc) -> { Label = toc.Label; Slug = slug })
            |> Seq.toArray
    })
    |> Seq.toArray

let navItems =
    pages
    |> Seq.ofArray
    |> Seq.map (fun page ->
        match page with
        | {
              Metadata = Some { Navbar = Some nav }
          } -> Some(page.Slug, nav)
        | _ -> None)
    |> Seq.choose id
    |> Seq.sortBy (fun (_, nav) -> nav.Order)
    |> Seq.map (fun (slug, nav) -> { Label = nav.Label; Slug = slug })
    |> Seq.toArray

type Helpers() =
    static member urlTo target = slugToFile target

    static member linkTo target =
        "<a href='" + (Helpers.urlTo target) + "'>" + target.asString + "</a>"

    static member groupContains group (slug: DocPart.Slug) =
        group.Items |> Array.exists (fun item -> item.Slug = slug)

pages
|> Array.iter (fun docPart ->
    let context = new TemplateContext()
    context.StrictVariables <- true

    let so = new ScriptObject()

    so.Import(
        {|
            title = docPart.Metadata.Value.Title
            description = docPart.Metadata.Value.Title
            rootUrl = "./"
            repoRoot = "repo"
            projectName = "ANSIConsoleToHtml"
            mainContent = docPart.Content
            slug = docPart.Slug
            toc = toc
            navItems = navItems
        |}
    )

    so.Import(typeof<Helpers>)

    context.PushGlobal(so)

    let templatedContent = mainLayout.Render(context)

    let fileName = Path.Combine(docFolder, slugToFile docPart.Slug)
    File.WriteAllText(fileName, templatedContent)
    printfn $"Created '{toRelative fileName}'")

exit 0
