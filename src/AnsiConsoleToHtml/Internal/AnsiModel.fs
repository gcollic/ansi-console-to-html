module internal AnsiModel

open AnsiConsoleToHtml

type AnsiStyle = {
    Foreground: Option<Color>
    Background: Option<Color>
    Bold: bool
    Italic: bool
} with

    static member Empty = {
        Foreground = None
        Background = None
        Bold = false
        Italic = false
    }
