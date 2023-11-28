module internal AnsiModel

open AnsiConsoleToHtml

type UnderlineKind =
    | NoUnderline
    | StraightUnderline
    | DoubleUnderline
    | CurlyUnderline
    | DottedUnderline
    | DashedUnderline

type AnsiStyle = {
    Foreground: Option<Color>
    Background: Option<Color>
    Bold: bool
    Italic: bool
    Underline: UnderlineKind
    Strikethrough: bool
} with

    static member Empty = {
        Foreground = None
        Background = None
        Bold = false
        Italic = false
        Underline = NoUnderline
        Strikethrough = false
    }
