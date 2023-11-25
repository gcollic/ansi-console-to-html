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
        testList "ANSI colors table page" (AnsiColorsTablePage.pages () |> List.map verifyDocPart)
        testList
            "ANSI colors sequences page"
            (AnsiColorsSequencesPage.pages () |> List.map verifyDocPart)
        testList
            "ANSI text decoration page"
            (AnsiTextDecorationsPage.pages () |> List.map verifyDocPart)
        testList "Getting started page" (GettingStartedPage.pages () |> List.map verifyDocPart)
        testList
            "Sequences overview page"
            (SequencesOverviewPage.pages () |> List.map verifyDocPart)
    ]
