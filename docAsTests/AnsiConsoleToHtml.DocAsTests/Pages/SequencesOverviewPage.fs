module SequencesOverviewPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi-sequences-overview"

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
        Row.from (Direct 7, "Inverse colors", AnsiColorsSequencesPage.slug)
        Row.from (Direct 8, "Hidden (but selectable)")
        Row.from (Direct 9, "Crossed-out")
        Row.from (Direct 21, "Doubly underlined", AnsiTextDecorationsPage.slug)
        Row.from (Reset 22, "Neither bold nor faint")
        Row.from (Reset 23, "Not italic")
        Row.from (Reset 24, "Not underlined", AnsiTextDecorationsPage.slug)
        Row.from (Prefix(27, "7"), "Not inversed", AnsiColorsSequencesPage.slug)
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
    |> examplesToMarkdownDocPart (slug + "_graphic")

let unsupportedGraphicOverview =
    [
        (5, "Slow blink")
        (6, "Rapid blink")
        (10, "Default font")
        (11, "Alternative font 1")
        (12, "Alternative font 2")
        (13, "Alternative font 3")
        (14, "Alternative font 4")
        (15, "Alternative font 5")
        (16, "Alternative font 6")
        (17, "Alternative font 7")
        (18, "Alternative font 8")
        (19, "Alternative font 9")
        (20, "Fraktur/Gothic font")
        (25, "Not blinking")
        (26, "Proportional spacing")
        (50, "Disable proportional spacing")
        (51, "Framed")
        (52, "Encircled")
        (53, "Overlined")
        (54, "Neither framed nor encircled")
        (55, "Not overlined")
        (60, "Ideogram underline or right side line")
        (61, "Ideogram double underline, or double line on the right side")
        (62, "Ideogram overline or left side line")
        (63, "Ideogram double overline, or double line on the left side")
        (64, "Ideogram stress marking")
        (65, "No ideogram attributes")
        (73, "Superscript")
        (74, "Subscript")
        (75, "Neither superscript nor subscript")
    ]
    |> List.map (fun (code, description) -> ($"Hello \x1B[{code}m World", $"{code}: {description}"))
    |> examplesGroupedByResultInMarkdownDocPart (slug + "_unsupported_graphic")

let nonGraphicOverview =
    [
        ("Hello \x1B[3A World", "Cursor 3 times up")
        ("Hello \x1B[22;5H World", "Moves the cursor to row 22, column 5")
        ("Hello \x1B[2J World", "Clear entire screen")
        ("Hello \x1B World", "Invalid/unfinished sequence")
    ]
    |> examplesGroupedByResultInMarkdownDocPart (slug + "_non_graphic")


[<Tests>]
let tests =
    verifyListOfDocPart "ANSI sequences overview"
    <| [ graphicOverview; unsupportedGraphicOverview; nonGraphicOverview ]
