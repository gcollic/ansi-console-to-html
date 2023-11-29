module Colorizer

open ColorCode

let private clean escapedEsc (s: string) =
    let cleaned =
        s.Replace("\\", "\\\\").Replace("\x1B", escapedEsc).Replace("\"", "\\\"")

    "\"" + cleaned + "\""

let toDotNetString s = clean "\\x1B" s
let toUnixShellString (s: string) = clean "\\033" s
let toPowerShellString (s: string) = clean "`e" s

let inlineHtmlDotNetstring (s: string) =
    $"<code style='color:#A31515;'>{toDotNetString s}</code>"

let private formatter = new HtmlFormatter()

let html code =
    formatter.GetHtmlString(code, Languages.Html)

let cSharp code =
    formatter.GetHtmlString(code, Languages.CSharp)
