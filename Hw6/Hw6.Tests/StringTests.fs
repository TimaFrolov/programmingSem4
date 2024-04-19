module Hw6.String.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let TestOk () =
    calculate {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    }
    |> should equal "3"

[<Test>]
let TestFail () =
    calculate {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    }
    |> should equal ""
