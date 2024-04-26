module Hw7.Task2.Lazy

open ILazy

type 'a Lazy(producer: unit -> 'a) =
    let value: 'a option ref = ref None

    interface 'a ILazy with
        member this.Get() : 'a =
            match value.Value with
            | Some v -> v
            | None ->
                let v = producer ()
                value.Value <- Some v
                v
