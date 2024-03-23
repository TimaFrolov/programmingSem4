module Hw4.Task2

let func x l = List.map (fun y -> y * x) l
// func x l = List.map (fun y -> y * x) l
// func x = List.map (fun y -> y * x)
// func x = List.map (fun y -> x * y) multiplication for integers is commutative
// func x = List.map ((*) x)
// func x = x |> (*) |> List.map
// func = (*) >> List.map
let func2 = (*) >> List.map
