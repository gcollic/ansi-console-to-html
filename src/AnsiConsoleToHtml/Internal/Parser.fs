module internal Parser

open FParsec
open System

type Token =
    | RawText of string
    | RawNewLine
    | AnsiMCommand of int list list
    | NonPrintable

let private textParser =
    many1Satisfy (function
        | '\x1B'
        | '\n' -> false
        | _ -> true)
    |>> RawText

let private ansiEscapeParser =
    let graphicsCodeSequence =
        let intByColon = sepBy (puint8 |>> int) (pchar ':')
        let intByColonThenSemicolon = sepBy intByColon (pchar ';')
        pchar '[' >>. intByColonThenSemicolon .>> pchar 'm' |>> AnsiMCommand

    let otherCodeSequence =
        let inline isBetween first last input = input - first <= last - first

        pchar '['
        .>> manySatisfy (isBetween '0' '?')
        .>> manySatisfy (isBetween ' ' '/')
        .>> satisfy (isBetween '@' '~')
        >>% NonPrintable

    let unrecognizedCodeSequence = preturn NonPrintable

    pchar '\x1B'
    >>. (attempt graphicsCodeSequence
         <|> attempt otherCodeSequence
         <|> unrecognizedCodeSequence)

let private newlineParser = newline >>% RawNewLine

let private parser =
    many (textParser <|> ansiEscapeParser <|> newlineParser) .>> eof

let parse input =
    match run parser input with
    | Success(result, _, _) -> result
    | Failure(errorMsg, _, _) -> raise (InvalidOperationException(errorMsg))
