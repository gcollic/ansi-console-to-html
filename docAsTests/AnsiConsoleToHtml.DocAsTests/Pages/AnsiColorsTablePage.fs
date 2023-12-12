module AnsiColorsTablePage

open Expecto
open VerifyPages
open AnsiConsoleToHtml
open DocPart

let colors = AnsiConsole.Colors256()

let toCell (colors256: Color[]) i =
    let isLightBackground =
        match i with
        | x when x < 16 -> x > 7
        | x when x < 232 -> (x - 16) % 36 > 17
        | x -> x > 243

    let fontColor = if isLightBackground then "black" else "white"
    let spacing = if i < 10 then $"&nbsp;&nbsp;" else ""
    $"<td style='background:%s{(colors256[i]).AsHexColor()};color:%s{fontColor}'>%s{spacing}%i{i}</td>"

let colorsToRow colors indexes =
    indexes
    |> List.map (toCell colors)
    |> String.concat ""
    |> fun s -> $"<tr>%s{s}</tr>\n"

let colorsToHtmlTable colors indexes =
    indexes
    |> List.map (colorsToRow colors)
    |> String.concat ""
    |> fun s -> $"\n<table>\n%s{s}</table>\n"

let colorsToHtmlTableDocPart slug indexes = {
    Slug = Slug.from slug
    Metadata = None
    Content = colorsToHtmlTable colors indexes
    Format = Html
}

let codesToMarkdownTable codes =
    codes
    |> List.map (fun i ->
        let result = $"\x1B[%s{i}mHello" |> AnsiConsole.ToHtml |> _.Replace("\n", "")
        $"| %s{i} | %s{result} |")
    |> String.concat "\n"
    |> fun s -> $"| Code | Result |\n|---|---|\n%s{s}\n"

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI 256 colors table"
    <| [
        colorsToHtmlTableDocPart "16-color-table" [ [ 0..7 ]; [ 8..15 ] ]
        colorsToHtmlTableDocPart "216-color-table" [
            for rowIndex in 0..5 -> [ for col in 0..35 -> (rowIndex * 36 + col + 16) ]
        ]
        colorsToHtmlTableDocPart "grays-table" [ [ 232..255 ] ]
    ]
