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
    Dim: bool
    Bold: bool
    Italic: bool
    Underline: UnderlineKind
    UnderlineColor: Option<Color>
    Strikethrough: bool
} with

    static member Empty = {
        Foreground = None
        Background = None
        Dim = false
        Bold = false
        Italic = false
        Underline = NoUnderline
        UnderlineColor = None
        Strikethrough = false
    }
