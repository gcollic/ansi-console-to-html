module Tests

open Expecto
open AnsiConsoleToHtml

open VerifyTests
open VerifyExpecto
open Page

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()


let verifyHtml name (content: string) =
    let settings = VerifySettings(verifySettings)
    settings.UseFileName(name)
    let uselessNotEmptyVerifierParameter = "useless"
    Verifier.Verify(uselessNotEmptyVerifierParameter, content, "html", settings)

let testHtmlPart name content =
    testTask name { do! verifyHtml name content }

let testHtmlPage name (page: Page.Page) =
    testTask name { do! verifyHtml name page.yamlFrontMatter }

[<Tests>]
let tests =
    testList "samples" [
        testHtmlPart "greeting" <| Say.greetings "world"
        testHtmlPage "ansi_colors" AnsiColorsPage.page
    ]
