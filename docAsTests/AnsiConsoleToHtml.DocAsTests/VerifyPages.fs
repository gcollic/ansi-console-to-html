module Tests

open Expecto
open AnsiConsoleToHtml

open VerifyTests
open VerifyExpecto
open Page

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()


let verifyPage (page: Page.Page) =
    let settings = VerifySettings(verifySettings)
    settings.UseFileName(page.Slug.asString)
    let uselessNotEmptyVerifierParameter = "useless"
    Verifier.Verify(uselessNotEmptyVerifierParameter, page.yamlFrontMatter, "html", settings)

let testHtmlPage (page: Page.Page) =
    testTask page.Slug.asString { do! verifyPage page }

[<Tests>]
let tests =
    testList "samples" [
        testHtmlPage {
            Slug = Slug.from "greeting"
            Metadata = None
            Content = Say.greetings "world"
        }
        testHtmlPage AnsiColorsPage.page
    ]
