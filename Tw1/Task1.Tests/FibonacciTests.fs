module Tw1.Task1.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [ 4I, 2I; 10I, 10I; 40I, 44I; 1000000I, 1089154I ]
    |> List.map (fun (a,b) -> TestCaseData(a,b))

[<TestCaseSource("testCases")>]
let Test N expected =
    sum_of_even_fibonacci N |> should equal expected

[<Test>]
let TestRegression () = answer |> should equal 1089154I