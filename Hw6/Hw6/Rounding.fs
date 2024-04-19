module Hw6.Rounding

type FixedPoint(N: int, value: bigint) =
    member val N: int = N
    member val value: bigint = value

    static member (+)(x: FixedPoint, y: FixedPoint) : FixedPoint =
        x.value + y.value |> fun res -> FixedPoint(x.N, res)

    static member (-)(x: FixedPoint, y: FixedPoint) : FixedPoint =
        x.value - y.value |> fun res -> FixedPoint(x.N, res)

    static member (*)(x: FixedPoint, y: FixedPoint) : FixedPoint =
        (x.value * y.value) / (10I ** x.N) |> fun res -> FixedPoint(x.N, res)

    static member (/)(x: FixedPoint, y: FixedPoint) : FixedPoint =
        ((10I ** x.N) * x.value) / y.value |> fun res -> FixedPoint(x.N, res)

type RoundingBuilder(N: int) =
    let multiplier =
        let rec helper (n: int) acc x =
            match n with
            | 0 -> acc
            | _ when n % 2 = 0 -> helper (n / 2) acc (x * x)
            | _ -> helper (n / 2) (acc * x) (x * x)

        helper N 1. 10.

    let fixedPoint x = FixedPoint(N, x)

    member this.Bind(x: float, f: FixedPoint -> float) : float =
        x * multiplier |> bigint |> fixedPoint |> f

    member this.Return(x: FixedPoint) : float = (float x.value) / multiplier

let rounding N = RoundingBuilder(N)
