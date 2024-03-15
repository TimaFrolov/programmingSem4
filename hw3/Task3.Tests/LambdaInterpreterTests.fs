module Hw3.Task3.Tests

open NUnit.Framework
open FsUnit

let alphaEquivalenceTestCases =
    [ Var "x", Var "x", true
      Abstraction("x", Var "x"), Abstraction("y", Var "y"), true
      Apply(Var "f", Var "y"), Apply(Var "f", Var "y"), true
      Var "x", Var "y", false
      Abstraction("x", Var "y"), Abstraction("y", Var "y"), false
      Apply(Var "f", Var "y"), Apply(Var "g", Var "y"), false
      Apply(Var "f", Var "y"), Apply(Var "f", Var "z"), false
      Var "x", Apply(Var "x", Var "z"), false ]
    |> List.map (fun (input1, input2, expected) -> TestCaseData(input1, input2, expected))

[<TestCaseSource("alphaEquivalenceTestCases")>]
let TestAlphaEquivalence (e1: Lambda) (e2: Lambda) expected = e1 =~ e2 |> should equal expected

let substitutionTestCases =
    [ Var "x", "x", Var "y", Var "y"
      Var "x", "z", Var "y", Var "x"
      Abstraction("x", Var "x"), "x", Var "y", Abstraction("x", Var "x")
      Abstraction("y", Apply(Var("y"), Var("x"))), "x", Var("y"), Abstraction("z", Apply(Var("z"), Var("y"))) ]
    |> List.map (fun (input, variable, substitution, expected) -> TestCaseData(input, variable, substitution, expected))

[<TestCaseSource("substitutionTestCases")>]
let TestSubstitution (expr: Lambda) var sub expected =
    expr.substitute var sub =~ expected |> should be True

let reductionTestCases =
    [ Var "x", Var "x"
      Apply(Abstraction("x", Var "x"), Var "y"), Var "y"
      Apply(Abstraction("x", Apply(Var "x", Var "x")), Abstraction("x", Apply(Var "x", Var "x"))),
      Apply(Abstraction("x", Apply(Var "x", Var "x")), Abstraction("x", Apply(Var "x", Var "x")))
      Apply(Apply(Abstraction("x", Var("x")), Var "y"), Apply(Abstraction("x", Var("x")), Var "z")),
      Apply(Var "y", Apply(Abstraction("x", Var("x")), Var "z"))
      Apply(Var "y", Apply(Abstraction("x", Var("x")), Var "z")), Apply(Var "y", Var "z")
      Abstraction("x", Apply(Abstraction("y", Var("y")), Var "x")), Abstraction("x", Var "x") ]
    |> List.map (fun (input, expected) -> TestCaseData(input, expected))

[<TestCaseSource("reductionTestCases")>]
let TestReductionStep (expr: Lambda) expected =
    expr.reductionStep =~ expected |> should be True
