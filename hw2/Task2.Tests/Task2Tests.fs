module Hw2.Task2.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [ (fun x -> x * x),
      Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf)),
      Node(1, Node(4, Leaf, Leaf), Node(9, Leaf, Leaf))
      (+) 4, Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf)), Node(5, Node(6, Leaf, Leaf), Node(7, Leaf, Leaf)) ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("testCases")>]
let testMap (f: int -> int) tree expected =
    BinaryTree.map f tree |> should equal expected
