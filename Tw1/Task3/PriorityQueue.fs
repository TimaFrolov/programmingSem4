module Tw1.Task3.PriorityQueue

open Queue

type 'a PriorityQueue() =
    let mutable queues: (int * 'a Queue) list = []

    let lowestQueue () =
        try
            queues |> List.minBy fst
        with _ ->
            raise (System.InvalidOperationException("Queue is empty"))

    let removeQueueWithPriority N =
        queues <- queues |> List.filter (fst >> ((<>) N))

    member _.Insert(elem: 'a, priority: int) : unit =
        match queues |> List.tryFind (fst >> (=) priority) with
        | None ->
            let q = Queue()
            q.Insert elem
            queues <- (priority, q) :: queues
        | Some(_, q) -> q.Insert elem

    member _.Pop() : 'a =
        let priority, lowest = lowestQueue ()
        let result = lowest.Pop()

        let () =
            if lowest.Empty() then
                removeQueueWithPriority priority
            else
                ()

        result

    member _.Top() : 'a = lowestQueue () |> snd |> (_.Top())

    member _.Empty() : bool = List.isEmpty queues
