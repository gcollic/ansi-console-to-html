module AnsiColorsTablePage

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
    $"<td style='background:{(colors256[i]).AsHexColor()};color:{fontColor}'>{spacing}{i}</td>"

let colorsToRow colors indexes =
    indexes
    |> List.map (toCell colors)
    |> String.concat ""
    |> fun s -> $"<tr>{s}</tr>\n"

let colorsToHtmlTable colors indexes =
    indexes
    |> List.map (colorsToRow colors)
    |> String.concat ""
    |> fun s -> $"\n<table>\n{s}</table>\n"

let colorsToHtmlTableDocPart slug indexes = {
    Slug = Slug.from slug
    Metadata = None
    Content = colorsToHtmlTable colors indexes
    Format = Html
}

let codesToMarkdownTable codes =
    codes
    |> List.map (fun i ->
        let result = $"\x1B[{i}mHello" |> AnsiConsole.ToHtml |> _.Replace("\n", "")
        $"| {i} | {result} |")
    |> String.concat "\n"
    |> fun s -> $"| Code | Result |\n|---|---|\n{s}\n"

let title = "ANSI 256 colors table (8-bit)"
let slug = "ansi_colors_table"
let table16ColorsSlug = "16-color-table"
let table216ColorsSlug = "216-color-table"
let tableGraysSlug = "grays-table"

let pages () = [
    colorsToHtmlTableDocPart table16ColorsSlug [ [ 0..7 ]; [ 8..15 ] ]
    colorsToHtmlTableDocPart table216ColorsSlug [
        for rowIndex in 0..5 -> [ for col in 0..35 -> (rowIndex * 36 + col + 16) ]
    ]
    colorsToHtmlTableDocPart tableGraysSlug [ [ 232..255 ] ]
    {
        Slug = Slug.from slug
        Metadata =
            Some {
                Title = title
                Navbar = None
                Toc =
                    Some {
                        Parent = "ANSI escape sequences"
                        Label = "256 colors table"
                        Order = 100
                    }
            }
        Content =
            $"""
# {title}

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit)

## 0-15: named colors

The name of these colors are in the specification, but the actual colors depends on the terminal and user configuration.
0-7 are standard colors, and 8-15 high-intensity versions.

<div>{{{{include '{table16ColorsSlug}'}}}}</div>

## 16-231: 216 colors

It's a 6×6×6 color cube.

<div>{{{{include '{table216ColorsSlug}'}}}}</div>

## 232-255: gray

It's a scale of 24 shades of gray.

<div>{{{{include '{tableGraysSlug}'}}}}</div>
"""
        Format = Markdown
    }
]
