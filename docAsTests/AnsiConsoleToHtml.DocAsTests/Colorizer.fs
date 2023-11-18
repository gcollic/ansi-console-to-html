module Colorizer

open ColorCode

let toDotNetstring (s: string) =
    "\"" + s.Replace("\x1B", "\\x1B") + "\""

let inlineHtmlDotNetstring (s: string) =
    $"<code style='color:#A31515;'>{toDotNetstring s}</code>"

let private formatter = new HtmlFormatter()

let html code =
    formatter.GetHtmlString(code, Languages.Html)

let cSharp code =
    formatter.GetHtmlString(code, Languages.CSharp)
