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
    testTask part.Slug.asString {
        let settings = VerifySettings(verifySettings)
        settings.UseFileName(part.Slug.asString)
        let uselessNotEmptyVerifierParameter = "useless"

        do!
            Verifier.Verify(
                uselessNotEmptyVerifierParameter,
                part.yamlFrontMatter,
                part.Format.extension,
                settings
            )
    }

[<Tests>]
let tests =
    testList "Doc parts" [
        verifyDocPart {
            Slug = Slug.from "greeting"
            Metadata = None
            Content = Say.greetings "world"
            Format = Markdown
        }
        verifyDocPart AnsiColorsPage.page
    ]
