// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO
open System.Reflection
open System.Threading.Tasks
open Scriban
open Scriban.Runtime
open AnsiConsoleToHtml

open DocPart

let projectConfig = {|
    rootUrl = "."
    projectName = "ANSIConsoleToHtml"
    repoRoot = "https://github.com/gcollic/ANSIConsoleToHtml"
    licenseName = "MIT"
    version =
        Assembly.GetAssembly(typeof<Color>).GetName().Version
        |> fun v -> $"{v.Major}.{v.Minor}.{v.Build}"
|}

Environment.GetCommandLineArgs()
|> Array.iteri (fun index arg -> printfn $"arg[{index}] = {arg}")

let rootFolder = Directory.GetCurrentDirectory()
let toRelative path = Path.GetRelativePath(rootFolder, path)
let docFolder = Path.Combine(rootFolder, "doc")

docFolder
|> Directory.GetFiles
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
    [|
        Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocAsTests", "expectations")
        Path.Combine(rootFolder, "docAsTests", "AnsiConsoleToHtml.DocExporter", "handwritten")
    |]
    |> Array.collect Directory.GetFiles
    |> Array.append [| Path.Combine(rootFolder, "CHANGELOG.md") |]
    |> Array.map (fun file ->
        Deserializer.parseDocWithOptionalYamlFrontMatter file (File.ReadAllText file))

let pages = allParts |> Array.filter (fun p -> p.Metadata.IsSome)

let docPartsBySlug =
    allParts |> Array.map (fun page -> (page.Slug, page)) |> Map.ofArray

let templateLoader =
    { new ITemplateLoader with
        /// <summary>
        /// Gets an absolute path for the specified include template name. Note that it is not necessarely a path on a disk,
        /// but an absolute path that can be used as a dictionary key for caching)
        /// </summary>
        /// <param name="context">The current context called from</param>
        /// <param name="callerSpan">The current span called from</param>
        /// <param name="templateName">The name of the template to load</param>
        /// <returns>An absolute path or unique key for the specified template name</returns>
        member this.GetPath(context, callerSpan, templateName) = templateName

        /// <summary>
        /// Loads a template using the specified template path/key.
        /// </summary>
        /// <param name="context">The current context called from</param>
        /// <param name="callerSpan">The current span called from</param>
        /// <param name="templatePath">The path/key previously returned by <see cref="GetPath"/></param>
        /// <returns>The content string loaded from the specified template path/key</returns>
        member this.Load(context, callerSpan, templatePath) =
            let docPart = docPartsBySlug.[Slug.from templatePath]

            match docPart.Format with
            | Markdown -> Markdig.Markdown.ToHtml docPart.Content
            | _ -> docPart.Content

        member this.LoadAsync(context, callerSpan, templatePath) =
            this.Load(context, callerSpan, templatePath) |> ValueTask<string>
    }

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

    static member fileNameOf target = target + ".html"
    static member urlTo target = Helpers.fileNameOf target

    static member linkTo target =
        "<a href='" + (Helpers.urlTo target) + "'>" + target + "</a>"

    static member groupContains group (slug: DocPart.Slug) =
        group.Items |> Array.exists (fun item -> item.Slug = slug)

pages
|> Array.iter (fun docPart ->

    let context = new TemplateContext()
    context.TemplateLoader <- templateLoader
    context.StrictVariables <- true

    let so = new ScriptObject()
    so.Import(projectConfig)

    so.Import(
        {|
            title = docPart.Metadata.Value.Title
            description = docPart.Metadata.Value.Title
            mainInclude = docPart.Slug
            slug = docPart.Slug
            toc = toc
            navItems = navItems
        |}
    )

    so.Import(typeof<Helpers>)

    context.PushGlobal(so)

    let templatedContent = mainLayout.Render(context)

    let fileName = Path.Combine(docFolder, Helpers.fileNameOf (docPart.Slug.ToString()))
    File.WriteAllText(fileName, templatedContent)
    printfn $"Created '{toRelative fileName}'")

exit 0
