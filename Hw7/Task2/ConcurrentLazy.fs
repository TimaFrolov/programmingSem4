module Hw7.Task2.ConcurrentLazy

open ILazy

type 'a ConcurrentLazy(producer: unit -> 'a) =
    let value: 'a option ref = ref None

    interface 'a ILazy with
        member this.Get() : 'a =
            match value.Value with
            | Some v -> v
            | None ->
                lock value (fun () ->
                    match value.Value with
                    | Some v -> v
                    | None ->
                        let v = producer () in
                        let () = value.Value <- Some v in
                        v)
