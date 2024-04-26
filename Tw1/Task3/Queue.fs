module Tw1.Task3.Queue

type 'a QueueElement = QueueElement of ('a * 'a QueueElement option ref)

type 'a Queue() =
    let mutable head: 'a QueueElement option = None
    let mutable tail: 'a QueueElement option = None

    member _.Insert(elem: 'a) : unit =
        let newElement = Some(QueueElement(elem, ref None))

        match tail with
        | None -> head <- newElement
        | Some(QueueElement(tail)) -> (snd tail).Value <- newElement

        tail <- newElement

    member _.Pop() : 'a =
        match head with
        | None -> raise (System.InvalidOperationException("Queue is empty"))
        | Some(QueueElement(h)) ->
            head <- (snd h).Value
            fst h

    member _.Top() : 'a =
        match head with
        | None -> raise (System.InvalidOperationException("Queue is empty"))
        | Some(QueueElement(head)) -> fst head

    member _.Empty() : bool = Option.isNone head