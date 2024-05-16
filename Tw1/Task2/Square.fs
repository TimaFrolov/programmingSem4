module Tw1.Task2

open System

let generateSquare N =
    match N with
    | _ when N <= 0 -> raise (ArgumentException("N should be greater than 0"))
    | 1 -> "*\n" :> char seq
    | _ ->
        let topBottomLine = Seq.concat [ Seq.replicate N '*'; "\n" ]
        let middleLine = Seq.concat [ seq { '*' }; Seq.replicate (N - 2) ' '; "*\n" ]

        [ seq { topBottomLine }; Seq.replicate (N - 2) middleLine; [ topBottomLine ] ]
        |> Seq.concat
        |> Seq.concat
