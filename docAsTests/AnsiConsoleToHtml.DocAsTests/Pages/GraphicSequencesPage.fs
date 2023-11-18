module GraphicSequencesPage

open AnsiConsoleToHtml
open DocPart

let colors = AnsiConsole.Colors256()

let slug = "ansi-graphic-sequences"
let title = "Graphic sequence overview"

let overviewSlug = slug + "_overview"

let overview =
    [
        ("0", "Reset", "\x1B[32mHi \x1B[0mWorld")
        ("1", "Bold or intense", "Hi \x1B[1mWorld")
        ("30–37", "Set foreground color", "Hi \x1B[32mWorld")
    ]
    |> List.map (fun (n, description, example) ->
        let dotnet = Colorizer.inlineHtmlDotNetstring example
        let result = AnsiConsole.ToHtml example |> _.Replace("\n", "")
        $"| {n} | {description} | {dotnet} | {result} |")
    |> String.concat "\n"
    |> fun rows ->
        $"
| n | Description | Example | Rendered |
|---|-------------|---------|----------|
{rows}
"

let sgrSample = "\x1B[ n m" |> Colorizer.inlineHtmlDotNetstring

let pages () = [
    {
        Slug = Slug.from overviewSlug
        Metadata = None
        Content = overview
        Format = Markdown
    }
    {
        Slug = Slug.from slug
        Metadata =
            Some {
                Title = title
                Navbar = None
                Toc =
                    Some {
                        Parent = "ANSI escape sequences"
                        Label = title
                        Order = 2
                    }
            }
        Content =
            $"""
# {title}

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#SGR_(Select_Graphic_Rendition)_parameters)

The control sequence {sgrSample}, named Select Graphic Rendition (SGR), sets display attributes.
Several attributes can be set in the same sequence, separated by semicolons.
Each display attribute remains in effect until a following occurrence of SGR resets it.

## Overview

{{{{include '{overviewSlug}'}}}}

"""
        Format = Markdown
    }
]
