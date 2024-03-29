module Hw4.Task3.Command

type Command =
    | Exit
    | Add of string
    | GetByName of string
    | GetByPhone of string
    | Print
    | Save of string
    | ReadFrom of string

let parse (str: string) : Option<Command> =
    [ "exit", (fun _ -> Exit)
      "print", (fun _ -> Print)
      "add ", Add
      "get by name ", GetByName
      "get by phone ", GetByPhone
      "save ", Save
      "read ", ReadFrom ]
    |> Seq.choose (fun (key: string, command) ->
        if str.StartsWith key then
            str
            |> Seq.skip key.Length
            |> Seq.map string
            |> String.concat ""
            |> command
            |> Some
        else
            None)
    |> Seq.tryHead
