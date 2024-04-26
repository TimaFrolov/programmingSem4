module Hw7.Task2.ConcurrentLazy

open ILazy

type 'a ConcurrentLazy(producer: unit -> 'a) =
    let mutable value: 'a option = None
    let lockObject = obj ()

    interface 'a ILazy with
        member this.Get() : 'a =
            match value with
            | Some v -> v
            | None ->
                lock lockObject (fun () ->
                    match value with
                    | Some v -> v
                    | None ->
                        let v = producer () in
                        let () = value <- Some v in
                        v)
