module internal Interpreter

open AnsiConsoleToHtml
open AnsiModel
open Parser

let rec private ansiCodesToStyle (colors256: Color[]) codes style =
    match codes with
    | [38] :: [2] :: [r] :: [g] :: [b] :: tail -> ansiCodesToStyle colors256 tail { style with Foreground = Some {R=uint8 r; G=uint8 g; B=uint8 b} }
    | [38] :: [5] :: [i] :: tail -> ansiCodesToStyle colors256 tail { style with Foreground = Some (colors256[i]) }
    | [48] :: [2] :: [r] :: [g] :: [b] :: tail -> ansiCodesToStyle colors256 tail { style with Background = Some {R=uint8 r; G=uint8 g; B=uint8 b} }
    | [48] :: [5] :: [i] :: tail -> ansiCodesToStyle colors256 tail { style with Background = Some (colors256[i]) }
    | (code :: options) :: tail ->
        let newStyle =
            match code with
            | 0 -> AnsiStyle.Empty
            | 1 -> { style with Bold = true }
            | 3 -> { style with Italic = true }
            | 4 ->
                match options with
                | [] ->
                    match style.Underline with
                    | NoUnderline -> { style with Underline = StraightUnderline }
                    | _ -> style
                | [0] -> { style with Underline = NoUnderline }
                | [1] -> { style with Underline = StraightUnderline }
                | [2] -> { style with Underline = DoubleUnderline }
                | [3] -> { style with Underline = CurlyUnderline }
                | [4] -> { style with Underline = DottedUnderline }
                | [5] -> { style with Underline = DashedUnderline }
                | _ -> style
            | 9 -> { style with Strikethrough = true }
            | 21 -> { style with Underline = DoubleUnderline }
            | 23 -> { style with Italic = false }
            | 24 -> { style with Underline = NoUnderline }
            | 29 -> { style with Strikethrough = false }
            | x when (30 <= x && x <= 37) -> { style with Foreground = Some (colors256[x-30]) }
            | 39 -> { style with Foreground = None }
            | x when (40 <= x && x <= 47) -> { style with Background = Some (colors256[x-40]) }
            | 49 -> { style with Background = None }
            | x when (90 <= x && x <= 97) -> { style with Foreground = Some (colors256[x-82]) }
            | x when (100 <= x && x <= 107) -> { style with Background = Some (colors256[x-92]) }
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
