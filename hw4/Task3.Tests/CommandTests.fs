module Hw4.Task3.Tests.Command

open NUnit.Framework
open Hw4.Task3.Command
open FsUnit

let testCases =
    [ "exit", Exit |> Some
      "print", Print |> Some
      "add asdklasd", Add "asdklasd" |> Some
      "get by name Joe", GetByName "Joe" |> Some
      "get by phone +1234", GetByPhone "+1234" |> Some
      "save book.txt", Save "book.txt" |> Some
      "read book.txt", ReadFrom "book.txt" |> Some
      "asda", None ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("testCases")>]
let ParseTest str (result: Command option) = parse str |> should equal result
