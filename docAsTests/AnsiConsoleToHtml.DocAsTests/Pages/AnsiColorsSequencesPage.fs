module AnsiColorsSequencesPage

open DocPart
open SampleRenderer

let cartesianCodesToAsciiTable slug (xCodes: string list) (yCodes: string list) =
    let maxXLength = xCodes |> List.map _.Length |> List.max
    let maxYLength = yCodes |> List.map _.Length |> List.max
    let alternativeText = "^(;,;)^"

    let maxTextLength =
        let total = maxXLength + maxYLength + 1

        if total <= alternativeText.Length then
            total
        else
            max alternativeText.Length (maxXLength + 1)


    let header =
        xCodes
        |> List.map (fun x -> x.PadLeft(maxTextLength, ' '))
        |> String.concat ""
        |> (fun row -> new string (' ', maxYLength) + row)

    let toText (code: string) =
        if code.Length <= maxTextLength then
            code
        else
            alternativeText
        |> _.PadLeft(maxTextLength, ' ')

    let rows =
        yCodes
        |> List.map (fun y ->
            xCodes
            |> List.map (fun x -> $"{x};{y}")
            |> List.map (fun combo -> $"\x1B[{combo}m{toText combo}\x1B[0m")
            |> String.concat ""
            |> (fun row -> y.PadLeft(maxYLength, ' ') + row))

    header :: rows |> String.concat "\n" |> createSample slug

let cartesianSimpleCodesToAsciiTable slug (xCodes: int list) (yCodes: int list) =
    let xAsString = xCodes |> List.map _.ToString()
    let yAsString = yCodes |> List.map _.ToString()
    cartesianCodesToAsciiTable slug xAsString yAsString

let title = "ANSI colors sequences"
let slug = "ansi_colors_sequences"
let table4bitColorsSlug = slug + "-4bit-colors"
let table8bitColorsSlug = slug + "-8bit-colors"
let table24bitColorsSlug = slug + "-24bit-colors"

let pages () = [
    cartesianSimpleCodesToAsciiTable table4bitColorsSlug [ yield! [ 40..47 ]; yield! [ 100..107 ] ] [
        yield! [ 30..37 ]
        yield! [ 90..97 ]
    ]
    cartesianCodesToAsciiTable table8bitColorsSlug [
        "48;5;16"
        "48;5;17"
        "48;5;18"
        "48;5;19"
        "48;5;20"
        "48;5;21"
    ] [ "38;5;16"; "38;5;22"; "38;5;28"; "38;5;34"; "38;5;40"; "38;5;46" ]
    cartesianCodesToAsciiTable table24bitColorsSlug [
        "48;2;237;201;81"
        "48;2;235;104;65"
        "48;2;204;42;54"
        "48;2;79;55;45"
        "48;2;0;160;176"
    ] [
        "38;2;237;201;81"
        "38;2;235;104;65"
        "38;2;204;42;54"
        "38;2;79;55;45"
        "38;2;0;160;176"
    ]
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

## 4-bit colors: 30-37 / 40-47 / 90-97 / 100-107

Substract 'x' to get the actual index in the {{{{link_to '{AnsiColorsTablePage.slug}' '256 colors table'}}}}.

* Standard colors
    * foreground: 30-37 (substract 30 for color index)
    * background: 40-47 (substract 40 for color index)
* Bright colors
    * foreground: 90-97 (substract 82 for color index)
    * background: 100-107 (substract 92 for color index)

{{{{include '{table4bitColorsSlug}'}}}}

## 8-bit colors: 38;5;n / 48;5;n

'n' is the index in the {{{{link_to '{AnsiColorsTablePage.slug}' '256 colors table'}}}}.

{{{{include '{table8bitColorsSlug}'}}}}

## 24-bit colors: 38;2;r;g;b / 48;2;r;g;b

'r' 'g' and 'b' are the red, green, and blue components of the RGB color space.

{{{{include '{table24bitColorsSlug}'}}}}


"""
        Format = Markdown
    }
]
