module Hw9.Task1

open FParsec

type Lambda =
    | Var of string
    | App of Lambda * Lambda
    | Abs of string * Lambda

type Statement =
    | Let of string * Lambda
    | Eval of Lambda

let parseVar: Parser<string, unit> =
    many1 asciiLetter |>> (Seq.map string >> String.concat "")

let parseVarList = sepBy1 parseVar spaces1

let parseTerm, parseTermRef = createParserForwardedToRef ()

let parseAbstraction =
    pchar '\\' >>. parseVarList .>> pchar '.' .>>. parseTerm
    |>> (fun (vars, term) -> List.foldBack (fun var acc -> Abs(var, acc)) vars term)

let parseTermR =
    choice
        [ parseAbstraction
          (parseVar |>> Var)
          (pchar '(' >>. parseTerm .>> pchar ')') ]

parseTermRef.Value <-
    sepBy1 parseTermR (pchar ' ')
    |>> (fun terms -> List.fold (fun acc term -> App(acc, term)) (List.head terms) (List.tail terms))

let parseStatementNoEof =
    (attempt (
        pstring "let" >>. spaces1 >>. parseVar .>> spaces1 .>> pchar '=' .>> spaces1
        .>>. parseTerm
        |>> Let
     )
     <|> (parseTerm |>> Eval))

let parseStatement = parseStatementNoEof .>> eof

let parseProgram = sepBy1 parseStatementNoEof (pchar '\n') .>> eof
