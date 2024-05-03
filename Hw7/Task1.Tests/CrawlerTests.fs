module Hw7.Task1.Tests

open NUnit.Framework
open FsUnit

let getLinksTestCases =
    [ "<html></html>", []
      "<html><a href=\"http://example.com\"/></html>", [ "http://example.com" ]
      "<html><p class=\"<a href=\"http://example.com\"/></html>", [] ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("getLinksTestCases")>]
let TestGetAllLinks doc expected =
    doc |> getAllLinks |> Seq.toList |> should equal expected
