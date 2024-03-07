module Hw2.Task2.Tests

open NUnit.Framework
open FsUnit
open BinaryTree

let testCases =
    [ (fun x -> x * x),
      Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf)),
      Node(1, Node(4, Leaf, Leaf), Node(9, Leaf, Leaf))
      (fun _ -> failwith "This function should not be called"), Leaf, Leaf ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("testCases")>]
let testMap (f: int -> int) tree expected = map f tree |> should equal expected
