module internal Renderer

open System
open System.Web
open AnsiConsoleToHtml
open AnsiModel
open Interpreter

let private mapIfNotEmpty f x =
    if Seq.isEmpty x then None else Some(f x)

let private toTextDecoration (style: AnsiStyle) =
    let underlineStyle =
        match style.Underline with
        | StraightUnderline -> Some "solid"
        | DoubleUnderline -> Some "double"
        | CurlyUnderline -> Some "wavy"
        | DottedUnderline -> Some "dotted"
        | DashedUnderline -> Some "dashed"
        | _ -> None
        |> Option.map (fun x -> $"underline 1px {x} ")

    seq {
        if style.Strikethrough then
            yield "line-through"

        if underlineStyle.IsSome then
            yield underlineStyle.Value
    }
    |> mapIfNotEmpty (fun x ->
        let decoration = String.concat " " x
        $"text-decoration:{decoration};")

let private toHtmlStyle (style: AnsiStyle) =
    let textDecoration = toTextDecoration style

    seq {
        if style.Foreground.IsSome then
            yield $"color:{style.Foreground.Value.AsHexColor()};"

        if style.Background.IsSome then
            yield $"background:{style.Background.Value.AsHexColor()};"

        if style.Bold then
            yield "font-weight:900;"

        if style.Italic then
            yield "font-style:italic;"

        if textDecoration.IsSome then
            yield textDecoration.Value

    }
    |> mapIfNotEmpty String.Concat

let private toSpan ansiStyle innerContent =
    match toHtmlStyle ansiStyle with
    | None -> innerContent
    | Some style -> $"<span style='{style}'>{innerContent}</span>"

let rec private convertToHtmlParts tokens =
    match tokens with
    | NewLine :: tail -> "\n" :: convertToHtmlParts tail
    | Text(text, style) :: tail ->
        let escapedText = HttpUtility.HtmlEncode text
        let span = toSpan style escapedText
        span :: convertToHtmlParts tail
    | [] -> []

let convertStyledTextToHtml (colors256: Color[]) tokens =
    convertToHtmlParts tokens
    |> String.Concat
    |> fun str ->
        $"<pre style='color:{(colors256[15]).AsHexColor()};background:{(colors256[0]).AsHexColor()}'>\n"
        + str
        + "\n</pre>"
