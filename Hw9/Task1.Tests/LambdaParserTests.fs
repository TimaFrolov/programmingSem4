module Hw9.Task1.Tests

open NUnit.Framework
open FsUnit
open FParsec

let testCases =
    [ "x", Eval(Var "x")
      "let S = \\x y z.x z (y z)",
      Let("S", Abs("x", Abs("y", Abs("z", App(App(Var "x", Var "z"), App(Var "y", Var "z"))))))
      "let K = \\x y.x", Let("K", Abs("x", Abs("y", Var "x")))
      "S K K", Eval(App(App(Var "S", Var "K"), Var "K"))
      "let let = let", Let("let", Var "let")
      "let", Eval(Var("let")) ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("testCases")>]
let TestStatementParser test expected =
    match run parseStatement test with
    | Success(result, _, _) -> result |> should equal expected
    | Failure(error, _, _) -> Assert.Fail($"Failed to parse: {error}")

let programTestCases =
    [ "let S = \\x y z.x z (y z)\nlet K = \\x y.x\nS K K",
      [ Let("S", Abs("x", Abs("y", Abs("z", App(App(Var "x", Var "z"), App(Var "y", Var "z"))))))
        Let("K", Abs("x", Abs("y", Var "x")))
        Eval(App(App(Var "S", Var "K"), Var "K")) ] ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("programTestCases")>]
let TestProgramParser test expected =
    match run parseProgram test with
    | Success(result, _, _) -> result |> should equal expected
    | Failure(error, _, _) -> Assert.Fail($"Failed to parse: {error}")
