module AnsiColorsPage

open AnsiConsoleToHtml
open DocPart
open SampleRenderer

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

let cartesianCodesToAsciiTable slug (xCodes: int list) (yCodes: int list) =
    let xAsString = xCodes |> List.map _.ToString()
    let maxXLength = xAsString |> List.map _.Length |> List.max
    let yAsString = yCodes |> List.map _.ToString()
    let maxYLength = yAsString |> List.map _.Length |> List.max
    let maxCombinaisonLength = maxXLength + maxYLength + 1

    let header =
        xAsString
        |> List.map (fun x -> x.PadLeft(maxCombinaisonLength, ' '))
        |> String.concat ""
        |> (fun row -> new string (' ', maxYLength) + row)

    let rows =
        yAsString
        |> List.map (fun y ->
            xAsString
            |> List.map (fun x -> $"{x};{y}")
            |> List.map (fun combo ->
                $"\x1B[{combo}m{combo.PadLeft(maxCombinaisonLength, ' ')}\x1B[0m")
            |> String.concat ""
            |> (fun row -> y.PadLeft(maxYLength, ' ') + row))

    header :: rows |> String.concat "\n" |> createSample slug

let title = "ANSI colors"
let slug = "ansi_colors"
let table16ColorsSlug = "16-color-table"
let table216ColorsSlug = "216-color-table"
let tableGraysSlug = "grays-table"
let tableSequenceDirectColorsSlug = slug + "-sequence-direct-colors"

let pages () = [
    colorsToHtmlTableDocPart table16ColorsSlug [ [ 0..7 ]; [ 8..15 ] ]
    colorsToHtmlTableDocPart table216ColorsSlug [
        for rowIndex in 0..5 -> [ for col in 0..35 -> (rowIndex * 36 + col + 16) ]
    ]
    colorsToHtmlTableDocPart tableGraysSlug [ [ 232..255 ] ]
    cartesianCodesToAsciiTable tableSequenceDirectColorsSlug [
        yield! [ 40..47 ]
        yield! [ 100..107 ]
    ] [ yield! [ 30..37 ]; yield! [ 90..97 ] ]
    {
        Slug = Slug.from slug
        Metadata =
            Some {
                Title = title
                Navbar = None
                Toc =
                    Some {
                        Parent = "ANSI escape sequences"
                        Label = title
                        Order = 100
                    }
            }
        Content =
            $"""
# ANSI colors

## Sequences

### 30-37 / 40-47 / 90-97 / 100-107 direct colors

Substract 'x' to get the actual index in the 256 colors table.

* Standard colors
    * foreground: 30-37 (substract 30 for color index)
    * background: 40-47 (substract 40 for color index)
* Bright colors
    * foreground: 90-97 (substract 82 for color index)
    * background: 100-107 (substract 92 for color index)

{{{{include '{tableSequenceDirectColorsSlug}'}}}}

<div class="color-tables">

## 256 colors table (8 bits)

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit)

### 0-15: named colors

The name of these colors are in the specification, but the actual colors depends on the terminal and user configuration.
0-7 are standard colors, and 8-15 high-intensity versions.

<div>{{{{include '{table16ColorsSlug}'}}}}</div>

### 16-231: 216 colors

It's a 6×6×6 color cube.

<div>{{{{include '{table216ColorsSlug}'}}}}</div>

### 232-255: gray

It's a scale of 24 shades of gray.

<div>{{{{include '{tableGraysSlug}'}}}}</div>

</div>
"""
        Format = Markdown
    }
]
