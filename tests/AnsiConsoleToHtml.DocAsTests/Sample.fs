module Tests

open Expecto
open AnsiConsoleToHtml

open VerifyTests
open VerifyExpecto
open System.Text
open System.Globalization
open System.Text.RegularExpressions

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()

let removeDiacritics (text: string) =
    let builder = new StringBuilder()

    for c in text.Normalize(NormalizationForm.FormD) do
        match CharUnicodeInfo.GetUnicodeCategory(c) with
        | UnicodeCategory.NonSpacingMark -> ()
        | _ -> builder.Append(c) |> ignore

    builder.ToString().Normalize(NormalizationForm.FormC)

let toSnakeCase (input: string) =
    (Regex.Split(input, @"(?=[A-Z])|\W"))
    |> Array.filter (fun x -> not (System.String.IsNullOrWhiteSpace(x)))
    |> fun x -> System.String.Join("_", x)
    |> fun x -> x.ToLowerInvariant()

let toExpectationFileName (input: string) =
    input |> removeDiacritics |> toSnakeCase

let verifyHtmlPart (name: string) (content: string) =
    let settings = VerifySettings(verifySettings)
    settings.UseFileName(toExpectationFileName name)
    Verifier.Verify("useless", content, "html", settings)

let testHtmlPart name content =
    testTask name { do! verifyHtmlPart name content }

[<Tests>]
let tests =
    testList "samples" [ testHtmlPart "Greeting world should work" <| Say.greetings "world" ]
