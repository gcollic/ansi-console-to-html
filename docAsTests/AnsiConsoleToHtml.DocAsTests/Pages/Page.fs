module Page

open Legivel.Serialization
open System

type NavbarMetadata = { Label: string; Order: int }

type TocMetadata = {
    Parent: string
    Label: string
    Order: int
}

type PageMetadata = {
    Title: string
    Navbar: NavbarMetadata option
    Toc: TocMetadata option
} with
    // https://fjoppe.github.io/Legivel/
    // F# yaml support is not great and needs trade-off.
    // Legivel yaml deserialization library is F# fluent, but the trade-off is manual serialization.
    member x.toYaml =
        [
            Some $"Title: '{x.Title}'"
            Option.map
                (fun (nav: NavbarMetadata) ->
                    $"Navbar:\n  Label: '{nav.Label}'\n  Order: {nav.Order}")
                x.Navbar
            Option.map
                (fun (toc: TocMetadata) ->
                    $"Toc:\n  Parent: '{toc.Parent}'\n  Label: '{toc.Label}'\n  Order: {toc.Order}")
                x.Toc
        ]
        |> List.choose id
        |> String.concat "\n"

    static member fromYaml yaml =
        let result =
            DeserializeWithOptions<PageMetadata>
                [ MappingMode MapYaml.AndRequireFullProjection ]
                yaml

        match result with
        | [] -> raise (InvalidOperationException "No yaml page metadata found")
        | [ Error e ] -> raise (InvalidOperationException(e.Error.ToString()))
        | [ Success s ] -> s.Data
        | _ -> raise (InvalidOperationException "Multiple yaml page metadata found")

type Page = {
    Metadata: PageMetadata
    Content: string
} with

    member x.yamlFrontMatter =
        $"---
{x.Metadata.toYaml}
---
{x.Content}"
