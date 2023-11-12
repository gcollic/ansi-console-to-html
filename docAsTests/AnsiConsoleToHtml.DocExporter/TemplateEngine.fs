module TemplateEngine

open System.Threading.Tasks
open Scriban
open Scriban.Runtime
open DocPart
open SiteMap

type DocumentationGlobalConfig = {
    rootUrl: string
    projectName: string
    repoRoot: string
    licenseName: string
    version: string
}

type Helpers() =

    static member fileNameOf target = target + ".html"
    static member urlTo target = Helpers.fileNameOf target

    static member linkTo target =
        "<a href='" + (Helpers.urlTo target) + "'>" + target + "</a>"

    static member groupContains group (slug: DocPart.Slug) =
        group.Items |> Array.exists (fun item -> item.Slug = slug)

let templateRenderer (allParts: DocPart array) =

    let docPartsBySlug =
        allParts |> Array.map (fun page -> (page.Slug, page)) |> Map.ofArray

    let templateLoader =
        { new ITemplateLoader with
            /// <summary>
            /// Gets a unique key for the specified include template name.
            /// </summary>
            member this.GetPath(_, _, templateName) = templateName

            /// <summary>
            /// Loads a template using the specified template key.
            /// </summary>
            /// <param name="templatePath">The key previously returned by <see cref="GetPath"/></param>
            /// <returns>The content string loaded from the specified template key</returns>
            member this.Load(_, _, templateKey) =
                let docPart = docPartsBySlug[Slug.from templateKey]

                match docPart.Format with
                | Markdown -> Markdig.Markdown.ToHtml docPart.Content
                | _ -> docPart.Content

            member this.LoadAsync(context, callerSpan, templatePath) =
                this.Load(context, callerSpan, templatePath) |> ValueTask<string>
        }


    let applyLayoutToDoc
        (projectConfig: DocumentationGlobalConfig)
        (siteMap: SiteMap)
        (layout: Template)
        docPart
        =
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
                toc = siteMap.Toc
                navItems = siteMap.Nav
            |}
        )

        so.Import(typeof<Helpers>)

        context.PushGlobal(so)

        layout.Render(context)

    applyLayoutToDoc
