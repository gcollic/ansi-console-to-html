module DocSources

open System.IO

let sources rootFolder = {|
    targetDocFolder = Path.Combine(rootFolder, "doc")
    assetsFolder = Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "assets")
    partsFolders = [|
        Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
        Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "handwritten")
        Path.Combine(
            rootFolder,
            "docAsTests",
            "AnsiConsoleToHtml.DocExporter",
            "templates",
            "parts"
        )
    |]
    partsDirectFiles = [| Path.Combine(rootFolder, "CHANGELOG.md") |]
    mainLayoutFile =
        Path.Combine(
            rootFolder,
            "docAsTests",
            "AnsiConsoleToHtml.DocExporter",
            "templates",
            "mainLayout.sbnhtml"
        )
|}
