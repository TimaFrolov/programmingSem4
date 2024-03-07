module Hw2.Task3.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [ Add(Var 1, Var 2), 3
      Mul(Var 1, Var 2), 2
      Var 5, 5
      Add(Var 1, Mul(Var 2, Var 3)), 7 ]
    |> List.map (fun (expr, expected) -> TestCaseData(expr, expected))

[<TestCaseSource("testCases")>]
let testEval (expr: Expr) expected = expr.eval |> should equal expected
