module internal Interpreter

open AnsiConsoleToHtml
open AnsiModel
open Parser

let rec private ansiCodesToStyle (colors256: Color[]) codes style =
    match codes with
    | [code] :: tail ->
        let newStyle =
            match code with
            | 0 -> AnsiStyle.Empty
            | 1 -> { style with Bold = true }
            | x when (30 <= x && x <= 37) -> { style with Foreground = Some (colors256[x-30]) }
            | x when (40 <= x && x <= 47) -> { style with Background = Some (colors256[x-40]) }
            // Unsupported
            | _ -> style
        ansiCodesToStyle colors256 tail newStyle
    | _ ->  style

type StyledText =
    | Text of (string * AnsiStyle)
    | NewLine

let rec private interpretRec colors256 tokens currentStyle =
    match tokens with
    | NonPrintable :: tail -> interpretRec colors256 tail currentStyle
    | RawNewLine :: tail -> NewLine :: interpretRec colors256 tail currentStyle
    | RawText t :: tail -> Text(t, currentStyle) :: interpretRec colors256 tail currentStyle
    | AnsiMCommand args :: tail ->
        let newStyle = ansiCodesToStyle colors256 args currentStyle
        interpretRec colors256 tail newStyle
    | [] -> []

let interpretCommands colors256 tokens =
    interpretRec colors256 tokens AnsiStyle.Empty
