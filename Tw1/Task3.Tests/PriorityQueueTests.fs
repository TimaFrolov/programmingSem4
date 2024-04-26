module Tw1.Task3.PriorityQueue.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let EmptyPopThrows () =
    let queue = PriorityQueue()
    queue.Pop >> ignore |> should throw typeof<System.InvalidOperationException>
    
[<Test>]
let PopReturnsCorrectElement () =
    let queue = PriorityQueue()
    queue.Insert(1, 3)
    queue.Insert(2, 2)
    queue.Insert(3, 1)
    queue.Pop() |> should equal 3
    queue.Pop() |> should equal 2
    queue.Pop() |> should equal 1

[<Test>]
let TopReturnsCorrectElement () =
    let queue = PriorityQueue()
    queue.Insert(1, 3)
    queue.Insert(2, 2)
    queue.Insert(3, 1)
    queue.Top() |> should equal 3
    queue.Pop() |> should equal 3
    queue.Top() |> should equal 2
    queue.Pop() |> should equal 2
    queue.Top() |> should equal 1
    queue.Pop() |> should equal 1

[<Test>]
let EmptyTopThrows () =
    let queue = PriorityQueue()
    queue.Top >> ignore |> should throw typeof<System.InvalidOperationException>

[<Test>]
let EmptyIsEmpty () =
    let queue = PriorityQueue()
    queue.Empty() |> should equal true