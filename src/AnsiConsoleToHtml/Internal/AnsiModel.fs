module internal AnsiModel

open AnsiConsoleToHtml

type AnsiStyle = {
    Foreground: Option<Color>
    Background: Option<Color>
    Bold: bool
} with

    static member Empty = {
        Foreground = None
        Background = None
        Bold = false
    }
