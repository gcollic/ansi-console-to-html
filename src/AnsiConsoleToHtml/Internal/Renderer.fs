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
        |> Option.map (fun x ->
            let color =
                style.UnderlineColor
                |> Option.map (fun c -> " " + c.AsHexColor())
                |> Option.defaultValue ""

            $"underline 1px %s{x}%s{color}")

    seq {
        if style.Strikethrough then
            yield "line-through"

        if underlineStyle.IsSome then
            yield underlineStyle.Value
    }
    |> mapIfNotEmpty (fun x ->
        let decoration = String.concat " " x
        $"text-decoration:%s{decoration}")

let convertStyledTextToHtml (colors256: Color[]) tokens =

    let toColors (style: AnsiStyle) =
        let applyToFg f (fg, bg) = (f fg, bg)

        (style.Foreground, style.Background)
        |> applyToFg (fun fg ->
            if style.Dim then
                fg
                |> Option.defaultValue colors256[15]
                |> (fun c -> {
                    R = c.R / 2uy
                    G = c.G / 2uy
                    B = c.B / 2uy
                })
                |> Some
            else
                fg)
        |> (fun (fg, bg) ->
            match style with
            | { Inverse = true } ->
                (bg |> Option.defaultValue (colors256[0]) |> Some,
                 fg |> Option.defaultValue (colors256[15]) |> Some)
            | _ -> (fg, bg))
        |> applyToFg (fun fg ->
            if style.Hidden then
                Some "transparent"
            else
                fg |> Option.map _.AsHexColor())

    let toHtmlStyle (style: AnsiStyle) =
        let textDecoration = toTextDecoration style
        let (foreground, background) = toColors style

        seq {
            if foreground.IsSome then
                yield $"color:%s{foreground.Value}"

            if background.IsSome then
                yield $"background:%s{background.Value.AsHexColor()}"

            if style.Bold then
                yield "font-weight:900"

            if style.Italic then
                yield "font-style:italic"

            if textDecoration.IsSome then
                yield textDecoration.Value

        }
        |> mapIfNotEmpty (String.concat ";")

    let toSpan ansiStyle innerContent =
        match toHtmlStyle ansiStyle with
        | None -> innerContent
        | Some style -> $"<span style='%s{style}'>%s{innerContent}</span>"

    let rec convertToHtmlParts tokens =
        match tokens with
        | NewLine :: tail -> "\n" :: convertToHtmlParts tail
        | Text(text, style) :: tail ->
            let escapedText = HttpUtility.HtmlEncode text

            let span =
                match style with
                // to have different color for strikethrough and underline, you need to nest two spans
                | { Strikethrough = false }
                | { Underline = NoUnderline }
                | { UnderlineColor = None } -> toSpan style escapedText
                | _ ->
                    escapedText
                    |> toSpan {
                        AnsiStyle.Empty with
                            Underline = style.Underline
                            UnderlineColor = style.UnderlineColor
                    }
                    |> toSpan { style with Underline = NoUnderline }

            span :: convertToHtmlParts tail
        | [] -> []

    convertToHtmlParts tokens
    |> String.Concat
    |> fun str ->
        $"<pre style='color:%s{(colors256[15]).AsHexColor()};background:%s{(colors256[0]).AsHexColor()}'>\n"
        + str
        + "\n</pre>"
