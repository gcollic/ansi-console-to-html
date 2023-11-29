module AnsiTextDecorationsPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi_text_decorations"

let underlineOverview =
    [
        ("4", "straight underline")
        ("4:0", "no underline")
        ("4:1", "straight underline")
        ("4:2", "double underline")
        ("4:3", "curly underline")
        ("4:4", "dotted underline")
        ("4:5", "dashed underline")
        ("21", "double underline")
        ("24", "no underline")
    ]
    |> List.map (fun (code, description) ->
        let example =
            if description = "no underline" then
                $"\x1B[4mHello \x1B[{code}mWorld"
            else
                $"\x1B[{code}mHello World"

        (code, description, example))
    |> examplesToMarkdownDocPart (slug + "_underline")

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI text decorations sequences" <| [ underlineOverview ]
