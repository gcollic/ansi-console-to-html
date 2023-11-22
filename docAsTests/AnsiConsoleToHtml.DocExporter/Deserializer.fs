module Deserializer

open Legivel.Serialization
open System
open System.IO
open System.Text.RegularExpressions

open DocPart

let private pageMetadataFromYaml yaml =
    let result =
        DeserializeWithOptions<DocPart.PageMetadata>
            [ MappingMode MapYaml.AndRequireFullProjection ]
            yaml

    match result with
    | [] -> raise (InvalidOperationException "No yaml page metadata found")
    | [ Error e ] -> raise (InvalidOperationException(e.Error.ToString()))
    | [ Success s ] -> s.Data
    | _ -> raise (InvalidOperationException "Multiple yaml page metadata found")

let private frontMatterRegex =
    new Regex(
        "^\s*(?:---)(?<yaml>[\\s\\S]*?)(?:\n---)\s*(?<content>.*)$",
        RegexOptions.Compiled ||| RegexOptions.Singleline
    )

let parseDocWithOptionalYamlFrontMatter (path: string) fileContent =
    let slug =
        path
        |> Path.GetFileNameWithoutExtension
        |> fun s ->
            if s.EndsWith(".verified") then
                s.Substring(0, s.Length - 9)
            else
                s
        |> Slug.from

    let format = Path.GetExtension(path) |> _.Substring(1) |> PageFormat.from

    let m = frontMatterRegex.Match(fileContent)

    match m.Success with
    | true -> {
        Slug = slug
        Metadata = m.Groups.["yaml"].Value |> pageMetadataFromYaml |> Some
        Content = m.Groups.["content"].Value
        Format = format
      }
    | _ -> {
        Slug = slug
        Metadata = None
        Content = fileContent
        Format = format
      }
