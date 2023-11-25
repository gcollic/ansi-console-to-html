module VerifyPages

open Expecto
open VerifyTests
open VerifyExpecto

open DocPart

let verifySettings = VerifySettings()
verifySettings.UseDirectory("expectations")
verifySettings.DisableDiff()
EmptyFiles.FileExtensions.AddTextExtension("sample")


let verifyDocPart (part: DocPart) =
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

let verifyListOfDocPart name (parts: DocPart list) =
    testList name (List.map verifyDocPart parts)
