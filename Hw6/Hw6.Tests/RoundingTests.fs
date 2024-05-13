module Hw6.Rounding.Tests

open NUnit.Framework
open FsUnit

let delta N = (0.1 ** N) * (1. + 1e-9)

[<Test>]
let Test () =
    rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    - 0.048
    |> abs
    |> should lessThan (delta 3)
