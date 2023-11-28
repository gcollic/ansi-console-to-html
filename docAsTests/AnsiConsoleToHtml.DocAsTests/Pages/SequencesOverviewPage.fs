module SequencesOverviewPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi-sequences-overview"

let nonGraphicOverviewSlug = slug + "_non_graphic"
let graphicOverviewSlug = slug + "_graphic"

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
            Type = Direct 9
            Description = "Crossed-out"
            MoreDetails = None
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
            Type = Reset 29
            Description = "Not crossed out"
            MoreDetails = None
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
            | Reset x ->
                (x.ToString(), completeDescription, $"\x1B[44;33;1;3;4;9mHi \x1B[{x}mWorld")
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
    verifyListOfDocPart "ANSI sequences overview"
    <| [ graphicOverview; nonGraphicOverview ]
