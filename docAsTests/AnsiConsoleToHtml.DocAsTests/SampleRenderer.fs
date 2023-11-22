module SampleRenderer

open AnsiConsoleToHtml
open DocPart
open Sample

let createSample slug sample =
    let result = AnsiConsole.ToHtml sample
    let sample = { Input = sample; Output = result }

    {
        Slug = Slug.from slug
        Metadata = None
        Content = sample.serialize ()
        Format = Sample
    }
