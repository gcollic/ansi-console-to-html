module DocSources

open System.IO

let sources rootFolder =
    let docFolder = Path.Combine(rootFolder, "doc")

    {|
        targetDocFolder = Path.Combine(docFolder, "output")
        assetsFolder = Path.Combine(docFolder, "assets")
        partsFolders = [|
            Path.Combine(docFolder, "tests_expectations")
            Path.Combine(docFolder, "handwritten")
            Path.Combine(docFolder, "templates", "parts")
        |]
        partsDirectFiles = [| Path.Combine(rootFolder, "CHANGELOG.md") |]
        mainLayoutFile = Path.Combine(docFolder, "templates", "mainLayout.sbnhtml")
    |}
