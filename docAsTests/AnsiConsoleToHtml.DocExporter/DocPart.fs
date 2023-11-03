module DocPart

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

type Slug = private Slug of string with
    member this.asString =
        match this with
        | Slug s -> s
    static member from (s:string) = Slug (s.Trim())

type DocPart = {
    Slug: Slug
    Metadata: PageMetadata option
    Content: string
} with
    member this.yamlFrontMatter =
        match this.Metadata with
        | None -> this.Content
        | Some metadata -> $"---
{metadata.toYaml}
---
{this.Content}"
