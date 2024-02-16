module Hw1

let rec factorial =
    let rec helper acc =
        function
        | 0u -> acc
        | n -> helper (acc * n) (n - 1u)

    helper 1u

let fibonacci =
    let rec helper next current =
        function
        | 0u -> current
        | n -> helper (next + current) next (n - 1u)

    helper 1u 0u

let reverse list =
    let rec helper acc =
        function
        | [] -> acc
        | head :: tail -> helper (head :: acc) tail

    helper [] list

let powerList n m =
    List.unfold (fun (x, count) -> if count < 0 then None else Some(x, (x * 2, count - 1))) (pown 2 n, m - n)

let findIndex x =
    let rec helper acc =
        function
        | [] -> None
        | head :: _ when head = x -> Some acc
        | _ :: tail -> helper (acc + 1) tail

    helper 0
