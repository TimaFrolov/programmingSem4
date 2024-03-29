module Hw5.Simulator.Tests

open NUnit.Framework
open FsUnit

type OS =
    | Windows
    | Linux

let testCases =
    [ [ (Windows, Linux, 1.0); (Linux, Windows, 1.0) ],
      [ (1, Windows); (2, Linux); (3, Linux) ],
      [ 1, [ 2; 3 ]; 2, [ 1 ]; 3, [ 1 ] ],
      [ 2 ],
      [ [ 1 ]; [ 3 ] ]
      [ (Windows, Windows, 1.0); (Windows, Linux, 0.0); (Linux, Windows, 0.0) ],
      [ (1, Windows); (2, Windows); (3, Linux) ],
      [ 1, [ 2; 3 ]; 2, [ 1 ]; 3, [ 1 ] ],
      [ 2 ],
      [ [ 1 ] ] ]
    |> List.map (fun (a, b, c, d, e) ->
        ({ new NetworkInfo<OS, int> with
            member _.getProbability start target =
                a
                |> Seq.choose (fun (x, y, z) -> if x = start && y = target then Some z else None)
                |> Seq.head

            member _.getOperatingSystem computer =
                b
                |> Seq.choose (fun (x, y) -> if x = computer then Some y else None)
                |> Seq.head

            member _.getConnected computer =
                c
                |> Seq.choose (fun (x, y) -> if x = computer then Some y else None)
                |> Seq.head
                |> seq

            member _.infected() = d

         },
         e))
    |> List.map (fun (a, e) -> TestCaseData(a, e))

[<TestCaseSource("testCases")>]
let TestSimulator (network: NetworkInfo<OS, int>) (expected: int list list) =
    Simulator(
        { new Randomizer with
            member _.getNext() = 0.0 },
        network
    )
        .run ()
    |> Seq.truncate (List.length expected)
    |> List.ofSeq
    |> should equal expected
