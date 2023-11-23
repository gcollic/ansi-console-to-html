module internal Renderer

open System
open System.Web
open AnsiConsoleToHtml
open AnsiModel
open Interpreter

let private toHtmlStyle (style: AnsiStyle) =

    seq {
        if style.Foreground.IsSome then
            yield $"color:{style.Foreground.Value.AsHexColor()};"

        if style.Background.IsSome then
            yield $"background:{style.Background.Value.AsHexColor()};"

        if style.Bold then
            yield "font-weight:900;"

        if style.Italic then
            yield "font-style:italic;"
    }
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
