module Hw4.Task1

let bracePairs =
    seq {
        ('(', ')')
        ('[', ']')
        ('{', '}')
    }

let isClosingBrace x =
    bracePairs |> Seq.map snd |> Seq.contains x

let isOpeningBrace x =
    bracePairs |> Seq.map fst |> Seq.contains x

let isBracePair p = bracePairs |> Seq.contains p

let checkBraceSequence str =
    let rec helper acc str =
        if Seq.isEmpty str then
            acc = []
        else
            let tail = Seq.tail str

            match Seq.head str with
            | x when isOpeningBrace x -> helper (x :: acc) tail
            | x when isClosingBrace x -> isBracePair (List.head acc, x) && helper (List.tail acc) tail
            | _ -> helper acc tail

    helper [] str
