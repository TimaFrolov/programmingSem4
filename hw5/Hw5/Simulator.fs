module Hw5.Simulator

open System.Collections.Generic

type Randomizer =
    abstract member getNext: unit -> float

type NetworkInfo<'OS, 'Computer> =
    abstract member getProbability: start: 'OS -> target: 'OS -> float
    abstract member getOperatingSystem: 'Computer -> 'OS
    abstract member getConnected: 'Computer -> 'Computer seq
    abstract member infected: unit -> 'Computer seq

type Simulator<'OS, 'Computer>(randomizer: Randomizer, network: NetworkInfo<'OS, 'Computer>) =
    let infected = HashSet(network.infected ())

    member private sim.getConnectedToInfected() =
        infected
        |> Seq.map (fun source -> source |> network.getConnected |> Seq.map (fun target -> source, target))
        |> Seq.concat
        |> Seq.filter (snd >> infected.Contains >> not)
        |> Seq.map (fun (source, target) ->
            (target, network.getProbability (network.getOperatingSystem source) (network.getOperatingSystem target)))

    member sim.runStep() : 'Computer list * bool =
        let connectedToInfected =
            sim.getConnectedToInfected () |> Seq.filter (snd >> (<) 0) |> List.ofSeq

        let newlyInfected =
            connectedToInfected
            |> Seq.filter (fun (_, prob) -> randomizer.getNext () < prob) // Not in point-free so randomizer gets called every time
            |> Seq.map fst
            |> List.ofSeq

        infected.UnionWith newlyInfected
        newlyInfected, List.length connectedToInfected > 0

    member sim.run() =
        Seq.initInfinite (fun _ -> sim.runStep ()) |> Seq.takeWhile snd |> Seq.map fst
