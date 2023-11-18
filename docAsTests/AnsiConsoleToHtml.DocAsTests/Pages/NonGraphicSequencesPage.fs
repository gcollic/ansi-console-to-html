module NonGraphicSequencesPage

open AnsiConsoleToHtml
open DocPart

let colors = AnsiConsole.Colors256()

let title = "Non-graphic sequences"

let toDotNetstring (s: string) =
    let dotNetString = s.Replace("\x1B", "\\x1B")
    $"<code style='color:#A31515;'>\"{dotNetString}\"</code>"

let examples =
    [
        ("Hello \x1B[3A World", "Cursor 3 times up")
        ("Hello \x1B[22;5H World", "Moves the cursor to row 22, column 5")
        ("Hello \x1B[2J World", "Clear entire screen")
        ("Hello \x1B World", "Invalid/unfinished sequence")
    ]
    |> List.map (fun (sample, comment) ->
        (toDotNetstring sample, comment, AnsiConsole.ToHtml sample))
    |> List.groupBy (fun (_, _, result) -> result)
    |> List.map (fun (result, values) ->
        let tableContent =
            values
            |> List.map (fun (dotnet, comment, _) -> $"| {dotnet} | {comment} |")
            |> String.concat "\n"

        $"All the following sequences are rendered as {result}

| Input | Sequence meaning |
|-------|------------------|
{tableContent}
"   )
    |> String.concat "\n"

let pages () = [
    {
        Slug = Slug.from "ansi-non-graphic-sequences"
        Metadata =
            Some {
                Title = title
                Navbar = None
                Toc =
                    Some {
                        Parent = "ANSI escape sequences"
                        Label = title
                        Order = 1
                    }
            }
        Content =
            $"""
# {title}

Only the {{{{link_to '{GraphicSequencesPage.slug}' 'SGR (Select Graphic Rendition) sequences'}}}} are supported. The other ANSI escape sequences are ignored.

{examples}

"""
        Format = Markdown
    }
]
