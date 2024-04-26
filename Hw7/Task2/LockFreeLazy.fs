module Hw7.Task2.LockFreeLazy

open System.Threading
open ILazy

type 'a LockFreeLazy(producer: unit -> 'a) =
    let mutable value: 'a option = None

    interface 'a ILazy with
        member this.Get() : 'a =
            match value with
            | Some v -> v
            | None ->
                let v = producer ()

                match Interlocked.CompareExchange(&value, Some v, None) with
                | Some v -> v
                | None -> v
