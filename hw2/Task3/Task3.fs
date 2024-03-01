module Hw2.Task3

type Expr =
    | Var of int
    | Add of Expr * Expr
    | Mul of Expr * Expr

    static member eval =
        function
        | Var x -> x
        | Add(x, y) -> Expr.eval x + Expr.eval y
        | Mul(x, y) -> Expr.eval x * Expr.eval y
