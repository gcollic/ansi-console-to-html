module AnsiColorsSequencesPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi_colors_sequences"

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI colors sequences"
    <| [
        cartesianSimpleCodesToSampleDocPart (slug + "-4bit-colors") [
            yield! [ 40..47 ]
            yield! [ 100..107 ]
        ] [ yield! [ 30..37 ]; yield! [ 90..97 ] ]
        cartesianCodesToSampleDocPart (slug + "-8bit-colors") [
            "48;5;16"
            "48;5;17"
            "48;5;18"
            "48;5;19"
            "48;5;20"
            "48;5;21"
        ] [ "38;5;16"; "38;5;22"; "38;5;28"; "38;5;34"; "38;5;40"; "38;5;46" ]
        cartesianCodesToSampleDocPart (slug + "-24bit-colors") [
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
        cartesianCodesToSampleDocPart (slug + "_underline") [
            "4"
            "4:1"
            "4:2"
            "4:3"
            "4:4"
            "4:5"
            "21"
            "4;9"
        ] [ "94"; "58;5;2"; "58;2;120;99;0"; "94;58;5;2" ]
        createSampleDocPart
            (slug + "_inverse")
            "            Invert default colors: Default    \x1B[7mInversed\x1B[0m
             Invert colorful text: \x1B[44;93mColor      \x1B[7mInversed\x1B[0m
Inverted dim, dims the background: \x1B[44;93;2mDim        \x1B[7mInversed\x1B[0m
  Inverted hidden, hides the text: \x1B[44;93;8mHidden     \x1B[7mInversed\x1B[0m
               Inverted underline: \x1B[44;93;4mUnderlined \x1B[7mInversed\x1B[0m
     Inverting twice does nothing: \x1B[7mOnce       \x1B[7mTwice   \x1B[0m"
    ]
