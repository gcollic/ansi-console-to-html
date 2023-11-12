namespace AnsiConsoleToHtml

[<Struct>]
type Color = {
    R: byte
    G: byte
    B: byte
} with

    member this.AsHexColor() = $"#{this.R:X2}{this.G:X2}{this.B:X2}"
