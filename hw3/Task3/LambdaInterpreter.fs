module Hw3.Task3

type Lambda =
    | Var of string
    | Apply of Lambda * Lambda
    | Abstraction of string * Lambda

    member x.freeVariables =
        match x with
        | Var x -> set [ x ]
        | Apply(e1, e2) -> Set.union e1.freeVariables e2.freeVariables
        | Abstraction(x, e) -> Set.remove x <| e.freeVariables

    member e1.substitute (x: string) (e2: Lambda) : Lambda =
        let freeVariables = e2.freeVariables

        let rec generateNew (occupied: string Set) x =
            if occupied.Contains x then
                generateNew occupied (x + "'")
            else
                x

        let rec helper =
            function
            | Var y when y = x -> e2
            | Var y -> Var y
            | Apply(e3, e4) -> Apply(helper e3, helper e4)
            | Abstraction(y, e3) when y = x -> Abstraction(y, e3)
            | Abstraction(y, e3) ->
                let z = generateNew (Set.union freeVariables e3.freeVariables) y
                Abstraction(z, e3.substitute y (Var z) |> helper)

        helper e1

    member x.reductionStep =
        let rec helper: Lambda -> bool * Lambda =
            function
            | Var x -> false, Var x
            | Apply(Abstraction(x, e1), e2) -> (true, e1.substitute x e2)
            | Apply(e1, e2) ->
                let reductionApplied, e1 = helper e1 in

                if reductionApplied then
                    (true, Apply(e1, e2))
                else
                    let reductionApplied, e2 = helper e2 in (reductionApplied, Apply(e1, e2))
            | Abstraction(x, e) -> let r, e = helper e in (r, Abstraction(x, e))

        x |> helper |> snd

    static member (=~)(e1, e2) =
        let rec helper (alpha: Map<string, string>) (alphaReverse: Map<string, string>) =
            function
            | Var x, Var y ->
                match alpha.TryFind x with
                | Some z -> y = z // x = y || alpha.TryFind x = Some y
                | None -> alphaReverse.TryFind y |> Option.isNone && x = y
            | Apply(e1, e2), Apply(e3, e4) -> helper alpha alphaReverse (e1, e3) && helper alpha alphaReverse (e2, e4)
            | Abstraction(x, e1), Abstraction(y, e2) -> helper (Map.add x y alpha) (Map.add y x alphaReverse) (e1, e2)
            | _, _ -> false

        helper Map.empty Map.empty (e1, e2)
