module Hw7.Task2.Tests

open System.Threading
open NUnit.Framework
open FsUnit
open Lazy
open ILazy
open ConcurrentLazy
open LockFreeLazy

type Counter() =
    let count = ref 0
    member this.Count() = count.Value

    member this.Call() =
        let _ = Interlocked.Increment(count)
        obj ()

let lazyConstructors =
    [ (fun f -> Lazy f :> obj ILazy)
      (fun f -> LockFreeLazy f)
      (fun f -> ConcurrentLazy f) ]
    |> List.map (fun a -> TestCaseData(a))

let threadSafeLazyConstructors =
    [ (fun f -> LockFreeLazy f :> obj ILazy); (fun f -> ConcurrentLazy f) ]
    |> List.map (fun a -> TestCaseData(a))

[<TestCaseSource("lazyConstructors")>]
let TestLazy (constructor: (unit -> obj) -> obj ILazy) =
    let counter = Counter()
    let lazyValue = constructor counter.Call

    Seq.initInfinite (fun _ -> lazyValue.Get())
    |> Seq.take 16
    |> Seq.distinct
    |> Seq.length
    |> should equal 1

    counter.Count() |> should equal 1

[<TestCaseSource("threadSafeLazyConstructors")>]
[<Repeat(40)>]
let TestThreadSafeLazy (constructor: (unit -> obj) -> obj ILazy) =
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
[<Repeat(40)>]
let TestConcurrentLazy () =
    let counter = Counter()
    let lazyValue: obj ILazy = ConcurrentLazy counter.Call

    Seq.initInfinite (fun _ -> async { return lazyValue.Get() })
    |> Seq.take 16
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length
    |> should equal 1

    counter.Count() |> should equal 1
