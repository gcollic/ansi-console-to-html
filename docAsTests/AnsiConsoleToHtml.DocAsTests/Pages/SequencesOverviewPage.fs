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

let graphicOverview =
    [
        ("0", "Reset", "\x1B[32;1;3mHi \x1B[0mWorld")
        ("1", "Bold or intense", "Hi \x1B[1mWorld")
        ("3", "Italic", "Hi \x1B[3mWorld")
        ("4",
         $"Underline (with optional style)<br/>{{{{link_to '{AnsiTextDecorationsPage.slug}' 'more details'}}}}",
         "Hi \x1B[4mWorld")
        ("21",
         $"Doubly underlined<br/>{{{{link_to '{AnsiTextDecorationsPage.slug}' 'more details'}}}}",
         "Hi \x1B[21mWorld")
        ("23", "Not italic", "\x1B[3mHi \x1B[23mWorld")
        ("24",
         $"Not underlined<br/>{{{{link_to '{AnsiTextDecorationsPage.slug}' 'more details'}}}}",
         "\x1B[4mHi \x1B[24mWorld")
        ("30–37",
         $"Set foreground color (standard)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[32mWorld")
        ("38",
         $"Set foreground color (38;5;n or 38;2;r;g;b)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[38;2;110;120;170mWorld")
        ("40–47",
         $"Set background color (standard)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[42mWorld")
        ("48",
         $"Set background color (48;5;n or 48;2;r;g;b)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[48;2;110;120;170mWorld")
        ("90–97",
         $"Set foreground color (bright)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[92mWorld")
        ("100–107",
         $"Set background color (bright)<br/>{{{{link_to '{AnsiColorsSequencesPage.slug}' 'more details'}}}}",
         "Hi \x1B[102mWorld")
    ]
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
