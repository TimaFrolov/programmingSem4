module Hw2.Task4

let primeNumbers =
    let isPrime n =
        { 2 .. n |> float |> sqrt |> int } |> Seq.forall (fun x -> n % x <> 0)

    Seq.initInfinite id |> Seq.skip 2 |> Seq.filter isPrime
