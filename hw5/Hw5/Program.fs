module Program

open System
open Hw5.Simulator

type OS =
    | Windows
    | Linux


let networkInfo =
    { new NetworkInfo<OS, int> with
        member _.getProbability start target =
            match (start, target) with
            | Windows, Windows -> 0.75
            | Windows, Linux -> 0.5
            | Linux, Windows -> 0.5
            | Linux, Linux -> 0.6

        member _.getOperatingSystem computer =
            match computer with
            | 1 -> Windows
            | 2 -> Linux
            | 3 -> Windows
            | _ -> failwith "Invalid computer"

        member _.getConnected computer =
            match computer with
            | 1 -> [ 2; 3 ]
            | 2 -> [ 1 ]
            | 3 -> [ 1 ]
            | _ -> failwith "Invalid computer"

        member _.infected() = [ 3 ] }

let randomizer =
    let random = Random()

    { new Randomizer with
        member _.getNext() = random.NextDouble() }

[<EntryPoint>]
let main _ =
    let mutable infected = [ 3 ]
    let simulator = Simulator<OS, int>(randomizer, networkInfo)

    let () =
        simulator.run ()
        |> Seq.map (fun x -> infected <- infected @ x)
        |> Seq.fold (fun _ () -> printf $"Infected computers: %A{infected}\n") ()

    0
