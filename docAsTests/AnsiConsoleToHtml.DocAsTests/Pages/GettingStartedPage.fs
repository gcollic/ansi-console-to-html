module GettingStartedPage

open Expecto
open VerifyPages
open AnsiConsoleToHtml
open DocPart

let comparison =
    let sample = "Hi \x1B[32mWorld"

    let dotnet =
        sample
        |> Colorizer.toSingleLineDotNet
        |> Colorizer.toCommandInPre $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(\n    " "\n)"

    let result = AnsiConsole.ToHtml sample
    let html = result |> Colorizer.html

    let content =
        $"
<div>
    DotNet
{dotnet}
</div>
<div>
    HTML code result
{html}
</div>
<div>
    HTML render result
{result}
</div>"

    {
        Slug = Slug.from "getting_started_comparison"
        Metadata = None
        Content = content
        Format = Html
    }

[<Tests>]
let tests = verifyListOfDocPart "Getting started" <| [ comparison ]
