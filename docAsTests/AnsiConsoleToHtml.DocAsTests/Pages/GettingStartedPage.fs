module GettingStartedPage

open AnsiConsoleToHtml
open DocPart
open Colorizer

let colors = AnsiConsole.Colors256()

let title = "Getting started"
let comparisonSlug = "getting_started_comparison"
let sample = "Hi \x1B[32mWorld"

let dotnet =
    $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(
    {Colorizer.toDotNetstring sample}
)"
    |> Colorizer.cSharp

let result = AnsiConsole.ToHtml sample
let html = result |> Colorizer.html

let comparison =
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

let pages () = [
    {
        Slug = Slug.from comparisonSlug
        Metadata = None
        Content = comparison
        Format = Html
    }
    {
        Slug = Slug.from "getting_started"
        Metadata =
            Some {
                Title = title
                Navbar = Some { Label = "Documentation"; Order = 1 }
                Toc =
                    Some {
                        Parent = title
                        Label = "How to use"
                        Order = 1
                    }
            }
        Content =
            $"""
# {title}

<div>{{{{ include '{comparisonSlug}' }}}}<div>

"""
        Format = Markdown
    }
]
