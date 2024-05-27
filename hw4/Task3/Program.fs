module Program

open System
open System.IO
open Hw4.Task3.Command
open Hw4.Task3.Phonebook
open Hw4.Task3.Utils

let useCommand (phonebook: Phonebook) command =
    match command with
    | Exit -> None
    | Add(str) ->
        str
        |> fromString
        |> Result.map (fun x -> add x phonebook)
        |> Result.mapError ((+) "Phonebook entry given in wrong form: ")
        |> Some
    | GetByName(name) ->
        let _ = phonebook |> findPhoneOfName name |> List.map Console.WriteLine
        phonebook |> Ok |> Some
    | GetByPhone(phone) ->
        let _ = phonebook |> findNameOfPhone phone |> List.map Console.WriteLine
        phonebook |> Ok |> Some
    | Print ->
        let _ = phonebook |> List.map (toString >> Console.WriteLine)
        phonebook |> Ok |> Some
    | Save(filename) ->
        try
            let () = File.WriteAllLines(filename, phonebook |> List.map toString)
            phonebook |> Ok |> Some
        with err ->
            "Error writing phonebook to file: " + err.Message |> Error |> Some
    | ReadFrom(filename) ->
        try
            File.ReadAllLines(filename)
            |> Seq.map fromString
            |> processResults
            |> Result.mapError ((+) "Incorrect entry in file: ")
            |> Some
        with _ ->
            "Error reading file" |> Error |> Some

let rec mainLoop phonebook =
    match Console.ReadLine() |> parse with
    | None -> let () = Console.WriteLine "Incorrect command" in mainLoop phonebook
    | Some command ->
        match useCommand phonebook command with
        | None -> 0
        | Some(Error err) -> let () = Console.WriteLine err in mainLoop phonebook
        | Some(Ok phonebook) -> mainLoop phonebook

[<EntryPoint>]
let main _ =
    let () = Console.WriteLine "Welcome to Phonebook! You can use next commands:"
    let () = Console.WriteLine "`add <phone> <name>` - add entry to the phonebook"
    let () = Console.WriteLine "`print` - print phonebook to stdout"
    let () = Console.WriteLine "`get by name <name>` - get all phones of given name"
    let () = Console.WriteLine "`get by phone <phone>` - get all names of given phone"
    let () = Console.WriteLine "`save <filename>` - save phonebook to file"
    let () = Console.WriteLine "`read <filename>` - read phonebook from file"
    let () = Console.WriteLine "`exit` - exit from application"
    mainLoop Phonebook.Empty
