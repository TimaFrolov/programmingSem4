module Hw7.Task2.Tests

open NUnit.Framework
open FsUnit
open Lazy
open ILazy
open ConcurrentLazy
open LockFreeLazy

type Counter() =
    let mutable count = 0
    member this.Count() = count

    member this.Call() =
        count <- count + 1
        count

let lazyConstructors =
    [ (fun f -> Lazy f :> int ILazy)
      (fun f -> LockFreeLazy f)
      (fun f -> ConcurrentLazy f) ]
    |> List.map (fun a -> TestCaseData(a))

let threadSafeLazyConstructors = List.skip 1 lazyConstructors

[<TestCaseSource("lazyConstructors")>]
let TestLazy (constructor: (unit -> int) -> int ILazy) =
    let counter = Counter()
    let lazyValue = constructor counter.Call

    Seq.initInfinite (fun _ -> lazyValue.Get())
    |> Seq.take 16
    |> Seq.distinct
    |> Seq.length
    |> should equal 1

    counter.Count() |> should equal 1

[<TestCaseSource("threadSafeLazyConstructors")>]
let TestThreadSafeLazy (constructor: (unit -> int) -> int ILazy) =
    let counter = Counter()
    let lazyValue = constructor counter.Call

    Seq.initInfinite (fun _ -> async { return lazyValue.Get() })
    |> Seq.take 16
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length
    |> should equal 1

[<Test>]
let TestConcurrentLazy () =
    let counter = Counter()
    let lazyValue: int ILazy = ConcurrentLazy counter.Call

    let _ =
        Seq.initInfinite (fun _ ->
            async {
                let value = lazyValue.Get()
                value |> should equal 1
                counter.Count() |> should equal 1
            })
        |> Seq.take 16
        |> Async.Parallel
        |> Async.RunSynchronously

    counter.Count() |> should equal 1
