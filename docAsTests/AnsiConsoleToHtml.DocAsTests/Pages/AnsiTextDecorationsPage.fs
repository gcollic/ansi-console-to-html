module AnsiTextDecorationsPage

open Expecto
open VerifyPages
open ExampleRenderer

let slug = "ansi_text_decorations"

let underlineOverview =
    [
        ("4", "straight underline", None)
        ("4:0", "no underline", Some $"\x1B[4mHello \x1B[4:0mWorld")
        ("4:1", "straight underline", None)
        ("4:2", "double underline", None)
        ("4:3", "curly underline", None)
        ("4:4", "dotted underline", None)
        ("4:5", "dashed underline", None)
        ("21", "double underline", None)
        ("24", "no underline", Some $"\x1B[4mHello \x1B[24mWorld")
    ]
    |> List.map (fun (code, description, example) ->
        (code, description, Option.defaultValue $"\x1B[{code}mHello World" example))
    |> examplesToMarkdownDocPart (slug + "_underline")

[<Tests>]
let tests =
    verifyListOfDocPart "ANSI text decorations sequences" <| [ underlineOverview ]
