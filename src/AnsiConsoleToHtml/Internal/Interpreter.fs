module internal Interpreter

open AnsiConsoleToHtml
open AnsiModel
open Parser

type StyledText =
    | Text of (string * AnsiStyle)
    | NewLine

let private (|Between|_|) first last code =
    if first <= code && code <= last then Some() else None

let interpretCommands (colors256: Color[]) tokens =

    let (|RGB|_|) codes =
        match codes with
        | [head]::[2]::[r]::[g]::[b]::tail ->  Some (head, {R=uint8 r; G=uint8 g; B=uint8 b}, tail)
        | [head]::[5]::[i]::tail ->            Some (head, colors256[i]                     , tail)
        | _ -> None

    let rec applyCodesToStyle codes style =
        match codes with
        | RGB (38, color, tail) -> applyCodesToStyle tail { style with Foreground = Some color }
        | RGB (48, color, tail) -> applyCodesToStyle tail { style with Background = Some color }
        | RGB (58, color, tail) -> applyCodesToStyle tail { style with UnderlineColor = Some color }
        | (code :: options) :: tail ->
            match code with
            | 0 ->                     AnsiStyle.Empty
            | 1 ->                   { style with Bold = true }
            | 2 ->                   { style with Dim = true }
            | 3 ->                   { style with Italic = true }
            | 4 ->
                match options with
                | [] ->
                    match style.Underline with
                    | NoUnderline -> { style with Underline = StraightUnderline }
                    | _ ->             style
                | [0] ->             { style with Underline = NoUnderline }
                | [1] ->             { style with Underline = StraightUnderline }
                | [2] ->             { style with Underline = DoubleUnderline }
                | [3] ->             { style with Underline = CurlyUnderline }
                | [4] ->             { style with Underline = DottedUnderline }
                | [5] ->             { style with Underline = DashedUnderline }
                |  _  ->               style
            | 7  ->                  { style with Inverse = true }
            | 8  ->                  { style with Hidden = true }
            | 9  ->                  { style with Strikethrough = true }
            | 21 ->                  { style with Underline = DoubleUnderline }
            | 22 ->                  { style with Dim = false; Bold = false }
            | 23 ->                  { style with Italic = false }
            | 24 ->                  { style with Underline = NoUnderline }
            | 27 ->                  { style with Inverse = false }
            | 28 ->                  { style with Hidden = false }
            | 29 ->                  { style with Strikethrough = false }
            | Between 30 37   ->     { style with Foreground = Some colors256[code-30] }
            | 39 ->                  { style with Foreground = None }
            | Between 40 47   ->     { style with Background = Some colors256[code-40] }
            | 49 ->                  { style with Background = None }
            | 59 ->                  { style with UnderlineColor = None }
            | Between 90 97   ->     { style with Foreground = Some colors256[code-82] }
            | Between 100 107 ->     { style with Background = Some colors256[code-92] }
            | _  ->                    style
            |> applyCodesToStyle tail
        | _ -> style

    let interpretToken (texts, currentStyle) token =
        match token with
        | NonPrintable -> (texts, currentStyle)
        | RawNewLine -> (texts @ [NewLine], currentStyle)
        | RawText t -> (texts @ [Text(t, currentStyle)], currentStyle)
        | AnsiMCommand codes -> (texts, applyCodesToStyle codes currentStyle)

    List.fold interpretToken ([], AnsiStyle.Empty) tokens |> fst
