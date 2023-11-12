module Serializer

open DocPart

// Legivel yaml deserialization library is F# fluent, but the trade-off is manual serialization.
let private toYaml metadata =
    [
        Some $"Title: '{metadata.Title}'"
        Option.map
            (fun (nav: NavbarMetadata) -> $"Navbar:\n  Label: '{nav.Label}'\n  Order: {nav.Order}")
            metadata.Navbar
        Option.map
            (fun (toc: TocMetadata) ->
                $"Toc:\n  Parent: '{toc.Parent}'\n  Label: '{toc.Label}'\n  Order: {toc.Order}")
            metadata.Toc
    ]
    |> List.choose id
    |> String.concat "\n"

let toYamlFrontMatter (doc: DocPart) =
    match doc.Metadata with
    | None -> doc.Content
    | Some metadata ->
        $"---
{toYaml metadata}
---
{doc.Content}"
