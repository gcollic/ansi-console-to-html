module Colorizer

open ColorCode

let toDotNetString (s: string) =
    let cleaned = s.Replace("\\", "\\\\").Replace("\x1B", "\\x1B").Replace("\"", "\\\"")
    "\"" + cleaned + "\""

let toUnixShellString (s: string) =
    let cleaned = s.Replace("\\", "\\\\").Replace("\x1B", "\\033").Replace("\"", "\\\"")
    "\"" + cleaned + "\""

let inlineHtmlDotNetstring (s: string) =
    $"<code style='color:#A31515;'>{toDotNetString s}</code>"

let private formatter = new HtmlFormatter()

let html code =
    formatter.GetHtmlString(code, Languages.Html)

let cSharp code =
    formatter.GetHtmlString(code, Languages.CSharp)
