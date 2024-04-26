module Tw1.Task2.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [ 1, "*\n"; 2, "**\n**\n"; 4, "****\n*  *\n*  *\n****\n" ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("testCases")>]
let TestSquareGenerator N (expected: string) =
    generateSquare N |> List.ofSeq |> should equal (expected |> List.ofSeq)

let throwCases = [ 0; -1; -2; -3; -4; -5 ] |> List.map (fun a -> TestCaseData(a))

[<TestCaseSource("throwCases")>]
let TestSquareGeneratorThrows N =
    (fun () -> generateSquare N |> ignore) |> should throw typeof<System.ArgumentException>
