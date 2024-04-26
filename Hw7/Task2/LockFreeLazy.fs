module Hw7.Task2.LockFreeLazy

open System.Threading
open ILazy

type 'a LockFreeLazy(producer: unit -> 'a) =
    let value: 'a option ref = ref None

    interface 'a ILazy with
        member this.Get() : 'a =
            match value.Value with
            | Some v -> v
            | None ->
                let v = producer ()

                match Interlocked.CompareExchange(value, Some v, None) with
                | Some v -> v
                | None -> v
