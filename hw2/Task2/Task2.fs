module Hw2.Task2

type 'a binaryTree =
    | Leaf
    | Node of 'a * 'a binaryTree * 'a binaryTree

type BinaryTree =
    static member map f =
        function
        | Leaf -> Leaf
        | Node(value, left, right) -> Node(f value, BinaryTree.map f left, BinaryTree.map f right)
