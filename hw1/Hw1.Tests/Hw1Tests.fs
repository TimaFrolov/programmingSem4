module Hw1.Tests

open NUnit.Framework


[<TestCase(0u, 1u)>]
[<TestCase(1u, 1u)>]
[<TestCase(2u, 2u)>]
[<TestCase(3u, 6u)>]
[<TestCase(4u, 24u)>]
[<TestCase(5u, 120u)>]
[<TestCase(6u, 720u)>]
let FactorialTests (arg, expected) =
    Assert.That(factorial arg, Is.EqualTo expected)

[<TestCase(0u, 0u)>]
[<TestCase(1u, 1u)>]
[<TestCase(2u, 1u)>]
[<TestCase(3u, 2u)>]
[<TestCase(4u, 3u)>]
[<TestCase(5u, 5u)>]
[<TestCase(6u, 8u)>]
[<TestCase(7u, 13u)>]
[<TestCase(8u, 21u)>]
[<TestCase(9u, 34u)>]
[<TestCase(10u, 55u)>]
let FibonacciTests (arg, expected) =
    Assert.That(fibonacci arg, Is.EqualTo expected)

let ReverseTestCases =

    [ [], []
      [ 1 ], [ 1 ]
      [ 1; 2; 3 ], [ 3; 2; 1 ]
      [ 1; 2; 1; 4 ], [ 4; 1; 2; 1 ] ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("ReverseTestCases")>]
let ReverseTests list reversed =
    Assert.That(reverse list, Is.EqualTo reversed)

let PowerListTestCases =
    [ 1, 5, [ 2; 4; 8; 16; 32 ]; 7, 7, [ 128 ]; 3, 2, [] ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("PowerListTestCases")>]
let PowerListTests n m expected =
    Assert.That(powerList n m, Is.EqualTo expected)

let FindIndexTestCases =
    [ 2, [ 1; 2; 3 ], Some 1; 2, [ 1; 2; 3; 2 ], Some 1; 2, [ 1; 3; 5 ], None ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("FindIndexTestCases")>]
let FindIndexTests x list (expected: int Option) =
    Assert.That(findIndex x list, Is.EqualTo expected)
