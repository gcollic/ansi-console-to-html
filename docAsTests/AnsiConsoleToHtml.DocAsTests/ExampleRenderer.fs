module ExampleRenderer

open AnsiConsoleToHtml
open DocPart
open Sample

let createSampleDocPart slug sample =
    let result = AnsiConsole.ToHtml sample
    let sample = { Input = sample; Output = result }

    {
        Slug = Slug.from slug
        Metadata = None
        Content = sample.serialize ()
        Format = Sample
    }

let cartesianCodesToSampleDocPart slug (xCodes: string list) (yCodes: string list) =
    let maxXLength = xCodes |> List.map _.Length |> List.max
    let maxYLength = yCodes |> List.map _.Length |> List.max
    let alternativeText = " Test "

    let (useAlternativeText, maxTextLength) =
        let total = maxXLength + maxYLength + 1

        if total <= alternativeText.Length then
            (false, total)
        else
            (true, max alternativeText.Length (maxXLength + 1))


    let header =
        xCodes
        |> List.map (fun x -> x.PadLeft(maxTextLength, ' '))
        |> String.concat ""
        |> (fun row -> new string (' ', maxYLength) + row)

    let toText (code: string) =
        if useAlternativeText then alternativeText else code
        |> _.PadLeft(maxTextLength, ' ')

    let rows =
        yCodes
        |> List.map (fun y ->
            xCodes
            |> List.map (fun x -> $"%s{x};%s{y}")
            |> List.map (fun combo -> $"\x1B[%s{combo}m%s{toText combo}\x1B[0m")
            |> String.concat ""
            |> (fun row -> y.PadLeft(maxYLength, ' ') + row))

    header :: rows |> String.concat "\n" |> createSampleDocPart slug

let cartesianSimpleCodesToSampleDocPart slug (xCodes: int list) (yCodes: int list) =
    let xAsString = xCodes |> List.map _.ToString()
    let yAsString = yCodes |> List.map _.ToString()
    cartesianCodesToSampleDocPart slug xAsString yAsString


let examplesToMarkdownDocPart slug examples =
    examples
    |> List.map (fun ((code: string), (description: string), example) ->
        let dotnet = Colorizer.inlineHtmlDotNetstring example
        let result = AnsiConsole.ToHtml example |> _.Replace("\n", "")
        $"| %s{code} | %s{description} | %s{dotnet} | %s{result} |")
    |> String.concat "\n"
    |> fun rows ->
        $"
| n | Description | Example | Rendered |
|---|-------------|---------|----------|
%s{rows}"
    |> (fun table -> {
        Slug = Slug.from slug
        Metadata = None
        Content = table
        Format = Markdown
    })

let examplesGroupedByResultInMarkdownDocPart slug examples =
    examples
    |> List.map (fun (sample, comment) ->
        (Colorizer.inlineHtmlDotNetstring sample, comment, AnsiConsole.ToHtml sample))
    |> List.groupBy (fun (_, _, result) -> result)
    |> List.map (fun (result, values) ->
        let tableContent =
            values
            |> List.map (fun (dotnet, comment, _) -> $"| %s{dotnet} | %s{comment} |")
            |> String.concat "\n"

        $"All the following sequences are rendered as %s{result}

| Input | Sequence meaning |
|-------|------------------|
%s{tableContent}
"   )
    |> String.concat "\n"
    |> (fun content -> {
        Slug = Slug.from slug
        Metadata = None
        Content = content
        Format = Markdown
    })
