module Hw4.Task1.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [ "", true
      ")", false
      "()(())", true
      "([{}])", true
      "([{]})", false
      "((asdasdadsadsad)gfvsvsd)", true ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("testCases")>]
let TestBraceChecker str expected =
    checkBraceSequence str |> should equal expected
