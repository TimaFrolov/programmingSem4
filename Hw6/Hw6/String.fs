module Hw6.String

type Builder() =
    member this.Bind(x: string, f: bigint -> string) : string =
        let res = ref 0I in if bigint.TryParse(x, res) then f res.Value else ""

    member this.Return(x: bigint) : string = $"{x}"

let calculate = Builder()
