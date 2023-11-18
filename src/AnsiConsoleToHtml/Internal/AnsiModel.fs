module internal AnsiModel

open AnsiConsoleToHtml

type AnsiStyle = {
    Foreground: Option<Color>
} with

    static member Empty = { Foreground = None }
