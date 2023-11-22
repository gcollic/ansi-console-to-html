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
    | Sample

    member this.extension =
        match this with
        | Html -> "html"
        | Markdown -> "md"
        | Sample -> "sample"

    static member from(s: string) =
        match s.ToLowerInvariant() with
        | "md" -> Markdown
        | "sample" -> Sample
        | _ -> Html

type PageMetadata = {
    Title: string
    Navbar: NavbarMetadata option
    Toc: TocMetadata option
}

type Slug =
    private
    | Slug of string

    override this.ToString() =
        match this with
        | Slug s -> s

    static member from(s: string) = Slug(s.Trim().ToLowerInvariant())

let (|Slug|) (input: Slug) =
    match input with
    | Slug s -> Slug s

type DocPart = {
    Slug: Slug
    Metadata: PageMetadata option
    Content: string
    Format: PageFormat
}
