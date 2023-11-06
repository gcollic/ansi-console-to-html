module DocPart

type NavbarMetadata = { Label: string; Order: int }

type TocMetadata = {
    Parent: string
    Label: string
    Order: int
}

type PageFormat =
    | Html
    | Markdown

type PageMetadata = {
    Title: string
    Navbar: NavbarMetadata option
    Toc: TocMetadata option
} with
    // https://fjoppe.github.io/Legivel/
    // F# yaml support is not great and needs trade-off.
    // Legivel yaml deserialization library is F# fluent, but the trade-off is manual serialization.
    member this.toYaml =
        [
            Some $"Title: '{this.Title}'"
            Option.map
                (fun (nav: NavbarMetadata) ->
                    $"Navbar:\n  Label: '{nav.Label}'\n  Order: {nav.Order}")
                this.Navbar
            Option.map
                (fun (toc: TocMetadata) ->
                    $"Toc:\n  Parent: '{toc.Parent}'\n  Label: '{toc.Label}'\n  Order: {toc.Order}")
                this.Toc
        ]
        |> List.choose id
        |> String.concat "\n"

type Slug =
    private
    | Slug of string

    member this.asString =
        match this with
        | Slug s -> s

    static member from(s: string) = Slug(s.Trim().ToLowerInvariant())

type DocPart = {
    Slug: Slug
    Metadata: PageMetadata option
    Content: string
    Format: PageFormat
} with

    member this.yamlFrontMatter =
        match this.Metadata with
        | None -> this.Content
        | Some metadata ->
            $"---
{metadata.toYaml}
---
{this.Content}"
