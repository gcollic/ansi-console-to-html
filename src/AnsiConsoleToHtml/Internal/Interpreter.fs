module internal Interpreter

open AnsiConsoleToHtml
open AnsiModel
open Parser

let (|RGB|_|) (colors256: Color[]) codes =
    match codes with
    | [head] :: [2] :: [r] :: [g] :: [b] :: tail ->  Some (head, {R=uint8 r; G=uint8 g; B=uint8 b}, tail)
    | [head] :: [5] :: [i] :: tail -> Some (head, colors256[i], tail)
    | _ -> None

let rec private ansiCodesToStyle (colors256: Color[]) codes style =
    match codes with
    | RGB colors256 (38, color, tail) -> ansiCodesToStyle colors256 tail { style with Foreground = Some color }
    | RGB colors256 (48, color, tail) -> ansiCodesToStyle colors256 tail { style with Background = Some color }
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
    | _ -> style

type StyledText =
    | Text of (string * AnsiStyle)
    | NewLine

let interpretCommands colors256 tokens =
    let interpretToken (results, currentStyle) token =
        match token with
        | NonPrintable -> (results, currentStyle)
        | RawNewLine -> (results @ [NewLine], currentStyle)
        | RawText t -> (results @ [Text(t, currentStyle)], currentStyle)
        | AnsiMCommand codes -> (results, ansiCodesToStyle colors256 codes currentStyle)
    List.fold interpretToken ([], AnsiStyle.Empty) tokens |> fst
