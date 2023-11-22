module Tests

open Expecto
open VerifyTests
open VerifyExpecto

open DocPart

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()
EmptyFiles.FileExtensions.AddTextExtension("sample")


let verifyDocPart (part: DocPart.DocPart) =
    let slug = part.Slug.ToString()

    testTask slug {
        let settings = VerifySettings(verifySettings)
        settings.UseFileName(slug)
        let uselessNotEmptyVerifierParameter = "useless"

        do!
            Verifier.Verify(
                uselessNotEmptyVerifierParameter,
                Serializer.toYamlFrontMatter part,
                part.Format.extension,
                settings
            )
    }

[<Tests>]
let tests =
    testList "Doc parts" [
        testList "ANSI Colors Page" (AnsiColorsPage.pages () |> List.map verifyDocPart)
        testList "Getting started Page" (GettingStartedPage.pages () |> List.map verifyDocPart)
        testList
            "Sequences overview Page"
            (SequencesOverviewPage.pages () |> List.map verifyDocPart)
    ]
