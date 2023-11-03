// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

printfn "Hello World"

Environment.GetCommandLineArgs()
|> Array.iteri (fun index arg -> printfn $"arg[{index}] = {arg}")

let rootFolder = Directory.GetCurrentDirectory()

Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "assets")
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(rootFolder, "doc", fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{file}'"
    printfn $"    to '{targetPath}'")

Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "templates")
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(rootFolder, "doc", fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{file}'"
    printfn $"    to '{targetPath}'")

Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(rootFolder, "doc", fileName)
    File.Copy(file, targetPath, true)
    printfn $"Copied '{file}'"
    printfn $"    to '{targetPath}'")

exit 0
