module AnsiColorsPage

open AnsiConsoleToHtml
open DocPart

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

let colorsToTable colors indexes =
    indexes
    |> List.map (colorsToRow colors)
    |> String.concat ""
    |> fun s -> $"\n<table>\n{s}</table>\n"

let doc16Colors colors =
    [ [ 0..7 ]; [ 8..15 ] ]
    |> colorsToTable colors
    |> fun x ->
        "\n<h2>0-15: 16 colors</h2>\nTODO depends on OS, etc. Standard and high intensity colors (4 bits)."
        + x

let doc216Colors colors =
    [ for rowIndex in 0..5 -> [ for col in 0..35 -> (rowIndex * 36 + col + 16) ] ]
    |> colorsToTable colors
    |> fun s -> "\n<h2>16-231: 216 colors</h2>\n" + s

let docGrayScaleColors colors =
    [ [ 232..255 ] ]
    |> colorsToTable colors
    |> fun s -> "\n<h2>232-255: grayscale colors</h2>\n" + s

let colors = Colors256.Table()

let title = "ANSI 256 colors table"

let page = {
    Slug = Slug.from "ansi_colors"
    Metadata =
        Some {
            Title = title
            Navbar = None
            Toc =
                Some {
                    Parent = "ANSI commands"
                    Label = title
                    Order = 1
                }
        }
    Content =
        [
            "<h1>256 colors table (8 bits)</h1>\n Cf. <a href='https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit'>Wikipedia</a>"
            doc16Colors colors
            doc216Colors colors
            docGrayScaleColors colors
        ]
        |> String.concat "\n"
}
