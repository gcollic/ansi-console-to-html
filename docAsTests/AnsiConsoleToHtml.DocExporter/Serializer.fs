module Serializer

open DocPart

// Legivel yaml deserialization library is F# fluent, but the trade-off is manual serialization.
let private toYaml metadata =
    [
        Some $"Title: '%s{metadata.Title}'"
        Option.map
            (fun (nav: NavbarMetadata) ->
                $"Navbar:\n  Label: '%s{nav.Label}'\n  Order: %i{nav.Order}")
            metadata.Navbar
        Option.map
            (fun (toc: TocMetadata) ->
                $"Toc:\n  Parent: '%s{toc.Parent}'\n  Label: '%s{toc.Label}'\n  Order: %i{toc.Order}")
            metadata.Toc
    ]
    |> List.choose id
    |> String.concat "\n"

let toYamlFrontMatter (doc: DocPart) =
    match doc.Metadata with
    | None -> doc.Content
    | Some metadata ->
        $"---
%s{toYaml metadata}
---
%s{doc.Content}"
