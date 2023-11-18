module internal AnsiModel

open AnsiConsoleToHtml

type AnsiStyle = {
    Foreground: Option<Color>
    Bold: bool
} with

    static member Empty = { Foreground = None; Bold = false }
