module Tests

open Expecto
open VerifyTests
open VerifyExpecto

open AnsiConsoleToHtml
open DocPart

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()


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
        testList "AnsiColorsPage" (AnsiColorsPage.pages () |> List.map verifyDocPart)
    ]
