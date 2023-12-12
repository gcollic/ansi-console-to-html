module AnsiResetSequencesPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi_reset_sequences"

let lotOfFormats = "44;33;1;2;3;4;9;58;5;1"

let overview =
    let examples = [
        ("0", "0 resets all kind of attributes")
        ("41;0;32", "0 is valid in the middle of a sequence")
        ("41;;32", "empty attribute is the same as 0")
    ]

    let maxDescriptionLength =
        examples |> List.map (fun (format, desc) -> desc.Length) |> List.max

    examples
    |> List.map (fun (format, description) ->
        $"%s{description.PadLeft(maxDescriptionLength)}: \x1B[%s{lotOfFormats}mBefore    \x1B[%s{format}mAfter '%s{format}'\x1B[0m")
    |> String.concat "\n"

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI reset sequences" [ createSampleDocPart (slug + "_overview") overview ]
