module Hw2.Task1

let countEven = List.length << List.filter (fun x -> x % 2 = 0)
let countEven2 = List.map (fun x -> x % 2 = 0) >> List.filter id >> List.length
let countEven3 = List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0
