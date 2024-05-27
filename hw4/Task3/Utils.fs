module Hw4.Task3.Utils

let rec processResults seq =
    let rec helper acc seq =
        match Seq.tryHead seq with
        | Some(Ok x) -> seq |> Seq.tail |> helper (x :: acc)
        | Some(Error err) -> Error err
        | None -> Ok acc

    seq |> helper [] |> Result.map List.rev
