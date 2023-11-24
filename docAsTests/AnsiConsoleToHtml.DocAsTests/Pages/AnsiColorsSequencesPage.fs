module AnsiColorsSequencesPage

open AnsiConsoleToHtml
open DocPart
open SampleRenderer

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

let title = "ANSI colors sequences"
let slug = "ansi_colors_sequences"
let tableSequenceDirectColorsSlug = slug + "-direct-colors"

let pages () = [
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
                        Order = 3
                    }
            }
        Content =
            $"""
# {title}

## 30-37 / 40-47 / 90-97 / 100-107 direct colors

Substract 'x' to get the actual index in the 256 colors table.

* Standard colors
    * foreground: 30-37 (substract 30 for color index)
    * background: 40-47 (substract 40 for color index)
* Bright colors
    * foreground: 90-97 (substract 82 for color index)
    * background: 100-107 (substract 92 for color index)

{{{{include '{tableSequenceDirectColorsSlug}'}}}}
"""
        Format = Markdown
    }
]
