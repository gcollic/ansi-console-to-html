module AnsiColorsSequencesPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi_colors_sequences"
let table4bitColorsSlug = slug + "-4bit-colors"
let table8bitColorsSlug = slug + "-8bit-colors"
let table24bitColorsSlug = slug + "-24bit-colors"

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI colors sequences"
    <| [
        cartesianSimpleCodesToSampleDocPart table4bitColorsSlug [
            yield! [ 40..47 ]
            yield! [ 100..107 ]
        ] [ yield! [ 30..37 ]; yield! [ 90..97 ] ]
        cartesianCodesToSampleDocPart table8bitColorsSlug [
            "48;5;16"
            "48;5;17"
            "48;5;18"
            "48;5;19"
            "48;5;20"
            "48;5;21"
        ] [ "38;5;16"; "38;5;22"; "38;5;28"; "38;5;34"; "38;5;40"; "38;5;46" ]
        cartesianCodesToSampleDocPart table24bitColorsSlug [
            "48;2;237;201;81"
            "48;2;235;104;65"
            "48;2;204;42;54"
            "48;2;79;55;45"
            "48;2;0;160;176"
        ] [
            "38;2;237;201;81"
            "38;2;235;104;65"
            "38;2;204;42;54"
            "38;2;79;55;45"
            "38;2;0;160;176"
        ]
    ]
