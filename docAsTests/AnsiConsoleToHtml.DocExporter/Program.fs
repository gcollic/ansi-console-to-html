// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

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

[| mainLayout |]
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(docFolder, fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{toRelative file}'"
    printfn $"    to '{toRelative targetPath}'")

Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(docFolder, fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{toRelative file}'"
    printfn $"    to '{toRelative targetPath}'")

exit 0
