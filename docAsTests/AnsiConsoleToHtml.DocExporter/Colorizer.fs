module Colorizer

open System.Text.RegularExpressions
open AnsiConsoleToHtml
open ColorCode

let private colorize color s =
    $"<span style=\"color:%s{color}\">%s{s}</span>"

let private colorString = colorize "#A31515"
let private colorEscaped = colorize "#EE0000"
let private colorFunction = colorize "#001080"

let private between before after s = before + s + after

let private toEscapedString escapedEsc (s: string) =
    s
        .Replace("\\", "\\\\")
        .Replace("\"", "\\\"")
        .Replace("\x1B", colorEscaped escapedEsc)
    |> between "\"" "\""
    |> colorString

let toCommandInPre before after command =
    command |> between before after |> colorFunction |> between "<pre>" "</pre>"

let toSingleLineDotNet =
    toEscapedString "\\x1B" >> _.Replace("\n", (colorEscaped "\\n"))

let private afterNewLine = new Regex("(?<=\n)")

let toMultilineDotNetPre (s: string) =
    s
    |> afterNewLine.Split
    |> Array.map toSingleLineDotNet
    |> String.concat ("+\n")
    |> toCommandInPre $"%s{nameof AnsiConsole}.%s{nameof AnsiConsole.ToHtml}(" ")"

let toUnixShellPre = toEscapedString "\\033" >> toCommandInPre "printf " ""
let toPowershellPre = toEscapedString "`e" >> toCommandInPre "echo " ""

let inlineHtmlDotNetstring (s: string) =
    s |> toSingleLineDotNet |> between "<code>" "</code>"

let private formatter = new HtmlFormatter()

let html code =
    formatter.GetHtmlString(code, Languages.Html)
