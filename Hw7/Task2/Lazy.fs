module Hw7.Task2.Lazy

open ILazy

type 'a Lazy(producer: unit -> 'a) =
    let mutable value: 'a option = None

    interface 'a ILazy with
        member this.Get() : 'a =
            match value with
            | Some v -> v
            | None ->
                let v = producer ()
                value <- Some v
                v
