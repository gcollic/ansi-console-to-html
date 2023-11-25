module GettingStartedPage

open Expecto
open VerifyPages
open AnsiConsoleToHtml
open DocPart

let title = "Getting started"
let comparisonSlug = "getting_started_comparison"

let pageContent =
    $"""
# {title}

<div>{{{{ include '{comparisonSlug}' }}}}<div>

"""

let comparison =
    let sample = "Hi \x1B[32mWorld"

    let dotnet =
        $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(
    {Colorizer.toDotNetString sample}
)"
        |> Colorizer.cSharp

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
        Slug = Slug.from comparisonSlug
        Metadata = None
        Content = content
        Format = Html
    }

[<Tests>]
let tests =
    verifyListOfDocPart title
    <| [
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
            Content = pageContent
            Format = Markdown
        }
        comparison
    ]
