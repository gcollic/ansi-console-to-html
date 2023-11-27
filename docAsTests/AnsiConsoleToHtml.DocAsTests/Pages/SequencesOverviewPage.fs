module SequencesOverviewPage

open Expecto
open VerifyPages
open AnsiConsoleToHtml
open DocPart
open ExampleRenderer

let colors = AnsiConsole.Colors256()

let slug = "ansi-sequences-overview"
let title = "ANSI sequences overview"

let nonGraphicOverviewSlug = slug + "_non_graphic"
let graphicOverviewSlug = slug + "_graphic"

let sgrSample = "\x1B[ n m" |> Colorizer.inlineHtmlDotNetstring

let pageContent =
    $"""
# {title}

Only the <abbr title="Select Graphic Rendition">SGR</abbr> sequence is supported. The other ANSI escape sequences are ignored.

## Graphic sequence

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#SGR_(Select_Graphic_Rendition)_parameters)

The control sequence {sgrSample} (where `n` is one of the codes below) is named Select Graphic Rendition (SGR).
It sets display attributes. Several attributes can be set in the same sequence, separated by semicolons,
and (rarely) with options separated by colons.
Each display attribute remains in effect until a following occurrence of SGR explicitely resets it.

{{{{include '{graphicOverviewSlug}'}}}}

## Non-graphic sequences

{{{{include '{nonGraphicOverviewSlug}'}}}}

"""

type ExampleType =
    | Reset of int
    | Direct of int
    | Range of int * int
    | RGB of int

type Example = {
    Type: ExampleType
    Description: string
    MoreDetails: string option
}

let graphicOverview =
    [
        {
            Type = Reset 0
            Description = "Reset"
            MoreDetails = None
        }
        {
            Type = Direct 1
            Description = "Bold or intense"
            MoreDetails = None
        }
        {
            Type = Direct 3
            Description = "Italic"
            MoreDetails = None
        }
        {
            Type = Direct 4
            Description = "Underline (with optional style)"
            MoreDetails = Some AnsiTextDecorationsPage.slug
        }
        {
            Type = Direct 21
            Description = "Doubly underlined"
            MoreDetails = Some AnsiTextDecorationsPage.slug
        }
        {
            Type = Reset 23
            Description = "Not italic"
            MoreDetails = None
        }
        {
            Type = Reset 24
            Description = "Not underlined"
            MoreDetails = Some AnsiTextDecorationsPage.slug
        }
        {
            Type = Range(30, 37)
            Description = "Set foreground color (standard)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
        {
            Type = RGB 38
            Description = "Set foreground color (38;5;n or 38;2;r;g;b)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
        {
            Type = Reset 39
            Description = "Default foreground color"
            MoreDetails = None
        }
        {
            Type = Range(40, 47)
            Description = "Set background color (standard)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
        {
            Type = RGB 48
            Description = "Set background color (48;5;n or 48;2;r;g;b)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
        {
            Type = Reset 49
            Description = "Default background color"
            MoreDetails = None
        }
        {
            Type = Range(90, 97)
            Description = "Set foreground color (bright)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
        {
            Type = Range(100, 107)
            Description = "Set background color (bright)"
            MoreDetails = Some AnsiColorsSequencesPage.slug
        }
    ]
    |> List.map
        (fun
            {
                Type = typ
                Description = desc
                MoreDetails = details
            } ->
            let completeDescription =
                match details with
                | None -> desc
                | Some slug -> $"{desc}<br/>{{{{link_to '{slug}' 'more details'}}}}"

            match typ with
            | Reset x -> (x.ToString(), completeDescription, $"\x1B[44;33;1;3;4mHi \x1B[{x}mWorld")
            | Direct x -> (x.ToString(), completeDescription, $"Hi \x1B[{x}mWorld")
            | Range(x, y) -> ($"{x}–{y}", completeDescription, $"Hi \x1B[{x + 2}mWorld")
            | RGB x -> (x.ToString(), completeDescription, $"Hi \x1B[{x};2;110;120;170mWorld"))
    |> examplesToMarkdownDocPart graphicOverviewSlug

let nonGraphicOverview =
    [
        ("Hello \x1B[3A World", "Cursor 3 times up")
        ("Hello \x1B[22;5H World", "Moves the cursor to row 22, column 5")
        ("Hello \x1B[2J World", "Clear entire screen")
        ("Hello \x1B World", "Invalid/unfinished sequence")
    ]
    |> examplesGroupedByResultInMarkdownDocPart nonGraphicOverviewSlug

[<Tests>]
let tests =
    verifyListOfDocPart title
    <| [
        {
            Slug = Slug.from slug
            Metadata =
                Some {
                    Title = title
                    Navbar = None
                    Toc =
                        Some {
                            Parent = "ANSI escape sequences"
                            Label = "Overview"
                            Order = 1
                        }
                }
            Content = pageContent
            Format = Markdown
        }
        graphicOverview
        nonGraphicOverview
    ]
