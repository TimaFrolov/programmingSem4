module Hw2.Task4.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let testUntil100 () =
    primeNumbers |> Seq.takeWhile ((>) 100) |> should equal
    <| seq {
        2
        3
        5
        7
        11
        13
        17
        19
        23
        29
        31
        37
        41
        43
        47
        53
        59
        61
        67
        71
        73
        79
        83
        89
        97
    }
