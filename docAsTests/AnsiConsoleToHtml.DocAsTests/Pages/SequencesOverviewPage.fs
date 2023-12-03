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
    | Prefix of int * string
    | Range of int * int
    | RGB of int

type Row =
    static member from(typ: ExampleType, desc: string) =
        match typ with
        | Reset x -> (x.ToString(), desc, $"\x1B[44;33;1;2;3;4;9;58;5;1mHi \x1B[{x}mWorld")
        | Direct x -> (x.ToString(), desc, $"Hi \x1B[{x}mWorld")
        | Prefix(x, y) -> (x.ToString(), desc, $"\x1B[{y}mHi \x1B[{x}mWorld")
        | Range(x, y) -> ($"{x}–{y}", desc, $"Hi \x1B[{x + 2}mWorld")
        | RGB x ->
            let prefix = if x = 58 then "\x1B[4m" else ""
            (x.ToString(), desc, $"{prefix}Hi \x1B[{x};2;110;120;170mWorld")

    static member from(typ: ExampleType, desc: string, slug: string) =
        Row.from (typ, $"{desc}<br/>{{{{link_to '{slug}' 'more details'}}}}")

let graphicOverview =
    [
        Row.from (Reset 0, "Reset")
        Row.from (Direct 1, "Bold or intense")
        Row.from (Direct 2, "Faint/Dim")
        Row.from (Direct 3, "Italic")
        Row.from (Direct 4, "Underline (with optional style)", AnsiTextDecorationsPage.slug)
        Row.from (Direct 7, "Inverse foreground and background colors")
        Row.from (Direct 8, "Hidden (but selectable)")
        Row.from (Direct 9, "Crossed-out")
        Row.from (Direct 21, "Doubly underlined", AnsiTextDecorationsPage.slug)
        Row.from (Reset 22, "Neither bold nor faint")
        Row.from (Reset 23, "Not italic")
        Row.from (Reset 24, "Not underlined", AnsiTextDecorationsPage.slug)
        Row.from (Prefix(27, "7"), "Not inversed")
        Row.from (Prefix(28, "8"), "Not hidden")
        Row.from (Reset 29, "Not crossed out")
        Row.from (Range(30, 37), "Set foreground color (standard)", AnsiColorsSequencesPage.slug)
        Row.from (
            RGB 38,
            "Set foreground color (38;5;n or 38;2;r;g;b)",
            AnsiColorsSequencesPage.slug
        )
        Row.from (Reset 39, "Default foreground color")
        Row.from (Range(40, 47), "Set background color (standard)", AnsiColorsSequencesPage.slug)
        Row.from (
            RGB 48,
            "Set background color (48;5;n or 48;2;r;g;b)",
            AnsiColorsSequencesPage.slug
        )
        Row.from (Reset 49, "Default background color")
        Row.from (
            RGB 58,
            "Set underline color (58;5;n or 58;2;r;g;b)",
            AnsiColorsSequencesPage.slug
        )
        Row.from (Reset 59, "Default underline color")
        Row.from (Range(90, 97), "Set foreground color (bright)", AnsiColorsSequencesPage.slug)
        Row.from (Range(100, 107), "Set background color (bright)", AnsiColorsSequencesPage.slug)
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
    verifyListOfDocPart "ANSI sequences overview"
    <| [ graphicOverview; nonGraphicOverview ]
