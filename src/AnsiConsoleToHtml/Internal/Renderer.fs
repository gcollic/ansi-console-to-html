module internal Renderer

open System
open System.Web
open AnsiConsoleToHtml
open AnsiModel
open Interpreter

let private toHtmlStyle (style: AnsiStyle) =

    [
        Option.map (fun (c: Color) -> $"color:{c.AsHexColor()};") style.Foreground
        if style.Bold then Some "font-weight: 900;" else None
        Option.map
            (function
            | (c: Color) -> $"background:{c.AsHexColor()};")
            style.Background
    ]
    |> List.choose id
    |> String.Concat

let private toSpan ansiStyle innerContent =
    match toHtmlStyle ansiStyle with
    | "" -> innerContent
    | style -> $"<span style='{style}'>{innerContent}</span>"

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
