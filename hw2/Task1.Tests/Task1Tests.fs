module Hw2.Task1.Tests

open NUnit.Framework
open FsUnit
open FsCheck

[<Test>]
let CountEvenSameAsCountEven2 () =
    Check.QuickThrowOnFailure(fun x -> countEven x = countEven2 x)

[<Test>]
let CountEvenSameAsCountEven3 () =
    Check.QuickThrowOnFailure(fun x -> countEven x = countEven3 x)

let TestCases =
    List.allPairs
        [ countEven; countEven2; countEven3 ]
        [ [ 1; 2; 3; 4; 5; 6; 7; 8; 9; 10 ], 5
          [ 1; 3; 5; 7; 9 ], 0
          [ 2; 4; 6; 8; 10 ], 5
          [ 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11 ], 5
          [ 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 ], 6 ]
    |> List.map (fun (a, (b, c)) -> TestCaseData(a, b, c))

[<TestCaseSource("TestCases")>]
let CountEvenTest (func: int list -> int) input expected = func input |> should equal expected
