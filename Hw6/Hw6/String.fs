module Hw6.String

type Builder() =
    member this.Bind(x: string, f: bigint -> string) : string =
        match
            try
                bigint.Parse x |> Some
            with _ ->
                None
            |> Option.map f
        with
        | Some x -> x
        | None -> ""

    member this.Return(x: bigint) : string = $"{x}"

let calculate = Builder()
