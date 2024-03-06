module Hw2.Task3

type Expr =
    | Var of int
    | Add of Expr * Expr
    | Mul of Expr * Expr

    member this.eval =
        match this with
        | Var x -> x
        | Add(x, y) -> x.eval + y.eval
        | Mul(x, y) -> x.eval * y.eval
