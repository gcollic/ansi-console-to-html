module SiteMap

open DocPart

type TocItem = { Label: string; Slug: DocPart.Slug }
type TocGroup = { Name: string; Items: TocItem array }

type SiteMap = {
    Toc: TocGroup array
    Nav: TocItem array
} with

    static member from(pages: DocPart array) =
        let toc =
            pages
            |> Seq.ofArray
            |> Seq.map (fun page ->
                match page with
                | { Metadata = Some { Toc = Some t } } -> Some(page.Slug, t)
                | _ -> None)
            |> Seq.choose id
            |> Seq.groupBy (fun (_, toc) -> toc.Parent)
            |> Seq.sortBy (fun (key, _) ->
                match key with
                | "Getting started" -> 0
                | "Misc" -> 1000
                | _ -> 1)
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

        { Toc = toc; Nav = navItems }
