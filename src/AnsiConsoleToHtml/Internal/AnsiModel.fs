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
} with

    member this.EnableUnderline() =
        match this.Underline with
        | NoUnderline -> {
            this with
                Underline = StraightUnderline
          }
        | _ -> this

    static member Empty = {
        Foreground = None
        Background = None
        Bold = false
        Italic = false
        Underline = NoUnderline
    }
