module Hw2.Task2


module BinaryTree =
    type 'a BinaryTree =
        | Leaf
        | Node of 'a * 'a BinaryTree * 'a BinaryTree

    let rec map f =
        function
        | Leaf -> Leaf
        | Node(value, left, right) -> Node(f value, map f left, map f right)
