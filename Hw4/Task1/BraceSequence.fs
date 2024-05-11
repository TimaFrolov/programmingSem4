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
        match (Seq.tryHead str, acc) with
        | None, x -> x = []
        | Some x, _ when isOpeningBrace x -> helper (x :: acc) (Seq.tail str)
        | Some x, h :: tl when isClosingBrace x && isBracePair (h, x) -> helper tl (Seq.tail str)
        | Some x, _ when isClosingBrace x -> false
        | Some _, _ -> helper acc (Seq.tail str)

    helper [] str
