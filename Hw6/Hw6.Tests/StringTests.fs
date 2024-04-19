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
    |> should equal (Some "3")

[<Test>]
let TestFail () =
    calculate {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    }
    |> should equal None

[<Test>]
let TestFractional () =
    calculate {
        let! x = "1.5"
        return x
    }
    |> should equal None
