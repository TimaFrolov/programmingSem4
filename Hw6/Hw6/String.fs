module Hw6.String

type Builder() =
    member this.Bind(x: string, f: bigint -> string option) : string option =
        let res = ref 0I in if bigint.TryParse(x, res) then f res.Value else None

    member this.Bind(x: string option, f: bigint -> string option) : string option =
        x |> Option.map (fun x -> this.Bind(x, f)) |> Option.flatten

    member this.Return(x: bigint) : string option = Some $"{x}"

let calculate = Builder()
