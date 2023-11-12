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

    member this.extension =
        match this with
        | Html -> "html"
        | Markdown -> "md"

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

type DocPart = {
    Slug: Slug
    Metadata: PageMetadata option
    Content: string
    Format: PageFormat
}
