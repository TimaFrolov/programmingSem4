module Tw1.Task3.Queue.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let EmptyPopThrows () =
    let queue = Queue()
    queue.Pop >> ignore |> should throw typeof<System.InvalidOperationException>
    
[<Test>]
let PopReturnsCorrectElement () =
    let queue = Queue()
    queue.Insert(1)
    queue.Insert(2)
    queue.Insert(3)
    queue.Pop() |> should equal 1
    queue.Pop() |> should equal 2
    queue.Pop() |> should equal 3

[<Test>]
let TopReturnsCorrectElement () =
    let queue = Queue()
    queue.Insert(1)
    queue.Insert(2)
    queue.Insert(3)
    queue.Top() |> should equal 1
    queue.Pop() |> should equal 1
    queue.Top() |> should equal 2
    queue.Pop() |> should equal 2
    queue.Top() |> should equal 3
    queue.Pop() |> should equal 3

[<Test>]
let EmptyTopThrows () =
    let queue = Queue()
    queue.Top >> ignore |> should throw typeof<System.InvalidOperationException>

[<Test>]
let EmptyIsEmpty () =
    let queue = Queue()
    queue.Empty() |> should equal true
