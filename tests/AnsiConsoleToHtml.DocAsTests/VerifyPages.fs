module Tests

open Expecto
open AnsiConsoleToHtml

open VerifyTests
open VerifyExpecto

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()


let verifyHtmlPart (name: string) (content: string) =
    let settings = VerifySettings(verifySettings)
    settings.UseFileName(name)
    Verifier.Verify("useless", content, "html", settings)

let testHtmlPart name content =
    testTask name { do! verifyHtmlPart name content }

[<Tests>]
let tests =
    testList "samples" [
        testHtmlPart "greeting" <| Say.greetings "world"
        testHtmlPart "ansi_colors" AnsiColorsPage.pageContent
    ]
