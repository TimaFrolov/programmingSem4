module Hw4.Task3.Tests.UtilsTests

open NUnit.Framework
open Hw4.Task3.Utils
open FsUnit

let testCases =
    [ [ Ok 1; Ok 2; Ok 3 ], Ok [ 1; 2; 3 ]; [ Ok 1; Error 5; Ok 3 ], Error 5 ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("testCases")>]
let TestProcessResults (arg: Result<int, int> list) (result: Result<int list, int>) =
    processResults arg |> should equal result
