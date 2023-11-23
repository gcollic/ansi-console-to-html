open Scriban
open System.IO
open System.Reflection
open AnsiConsoleToHtml
open DocPart
open TemplateEngine
open VerboseIo

let projectConfig = {
    rootUrl = "."
    projectName = "ANSIConsoleToHtml"
    repoRoot = "https://github.com/gcollic/ANSIConsoleToHtml"
    licenseName = "MIT"
    version =
        Assembly.GetAssembly(typeof<Color>).GetName().Version
        |> fun v -> $"{v.Major}.{v.Minor}.{v.Build}"
}

let rootFolder = Directory.GetCurrentDirectory()

let io = VerboseIo.forFolder rootFolder

let sources = DocSources.sources rootFolder

sources.targetDocFolder |> Directory.GetFiles |> Array.iter io.delete

sources.assetsFolder
|> Directory.GetFiles
|> Array.iter (fun file ->
    let fileName = Path.GetFileName(file)
    let targetPath = Path.Combine(sources.targetDocFolder, fileName)
    io.copy file targetPath)

let allDocParts =
    sources.partsFolders
    |> Array.collect Directory.GetFiles
    |> Array.append sources.partsDirectFiles
    |> Array.map (fun file ->
        Deserializer.parseDocWithOptionalYamlFrontMatter file (File.ReadAllText file))

let siteMap = SiteMap.SiteMap.from allDocParts

let mainLayout = sources.mainLayoutFile |> File.ReadAllText |> Template.Parse

let templateRenderer = new TemplateRenderer(allDocParts)

let applyLayoutToDoc =
    templateRenderer.applyLayoutToDoc projectConfig siteMap mainLayout

allDocParts
|> Array.filter _.Metadata.IsSome
|> Array.iter (fun docPart ->
    let fileName =
        Path.Combine(sources.targetDocFolder, Helpers.fileNameOf (docPart.Slug.ToString()))

    applyLayoutToDoc docPart |> io.write fileName)

let unusedDocParts =
    allDocParts
    |> Array.map _.Slug.ToString()
    |> Array.filter (fun slug -> templateRenderer.UsedKeys |> Set.contains slug |> not)

unusedDocParts
|> Array.iter (printfn "\x1b[31mError: documentation part not used: %s\x1b[0m")

unusedDocParts
|> Array.isEmpty
|> not
|> fun missing -> if missing then 1 else 0
|> exit
