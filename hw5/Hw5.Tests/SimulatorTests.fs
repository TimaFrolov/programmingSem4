module Hw5.Simulator.Tests

open NUnit.Framework
open FsUnit

type OS =
    | Windows
    | Linux

let testCases =
    [ { new NetworkInfo<OS, int> with // Everybody gets infected sooner or later
          member _.getProbability start target =
              match (start, target) with
              | Windows, Linux -> 1.0
              | Linux, Windows -> 1.0
              | _ -> 0.0

          member _.getOperatingSystem computer =
              match computer with
              | 1 -> Windows
              | 2 -> Linux
              | 3 -> Linux
              | _ -> failwith "Invalid computer"

          member _.getConnected computer =
              match computer with
              | 1 -> [ 2; 3 ]
              | 2 -> [ 1 ]
              | 3 -> [ 1 ]
              | _ -> failwith "Invalid computer"

          member _.infected() = [ 2 ]

      },
      [ [ 1 ]; [ 3 ] ]
      { new NetworkInfo<OS, int> with // Some computers are infected and some are not because they have different opearting systems
          member _.getProbability start target =
              match (start, target) with
              | Windows, Windows -> 1.0
              | _ -> 0.0

          member _.getOperatingSystem computer =
              match computer with
              | 1 -> Windows
              | 2 -> Windows
              | 3 -> Linux
              | _ -> failwith "Invalid computer"

          member _.getConnected computer =
              match computer with
              | 1 -> [ 2; 3 ]
              | 2 -> [ 1 ]
              | 3 -> [ 1 ]
              | _ -> failwith "Invalid computer"

          member _.infected() = [ 2 ]

      },
      [ [ 1 ] ]
      { new NetworkInfo<OS, int> with // Some computers not connected with infected computers
          member _.getProbability start target = 1.0

          member _.getOperatingSystem computer =
              match computer with
              | 1 -> Windows
              | 2 -> Windows
              | 3 -> Linux
              | 4 -> Windows
              | 5 -> Linux
              | 6 -> Linux
              | 7 -> Linux
              | _ -> failwith "Invalid computer"

          member _.getConnected computer =
              match computer with
              | 1 -> [ 2; 3 ]
              | 2 -> [ 1 ]
              | 3 -> [ 1 ]
              | 4 -> [ 5 ]
              | 5 -> [ 4 ]
              | 6 -> [ 7 ]
              | 7 -> [ 6 ]
              | _ -> failwith "Invalid computer"

          member _.infected() = [ 2; 4 ]

      },
      [ [ 1; 5 ]; [ 3 ] ] ]
    |> List.map (fun (network, expected) -> TestCaseData(network, expected))


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
