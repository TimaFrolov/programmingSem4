module Hw4.Task3.Phonebook

open System

type Entry =
    struct
        val name: string
        val phone: string
        new(name, phone) = { name = name; phone = phone }
    end

type Phonebook = Entry list

let add entry (book: Phonebook) = entry :: book

let empty: Phonebook = []

let findPhoneOfName name (phonebook: Phonebook) =
    phonebook |> List.filter ((_.name) >> (=) name) |> List.map (_.phone)

let findNameOfPhone phone (phonebook: Phonebook) =
    phonebook |> List.filter ((_.phone) >> (=) phone) |> List.map (_.name)

let toString (x: Entry) : string = $"%s{x.phone} %s{x.name}"

let fromString (str: char seq) : Result<Entry, string> =
    let (sign: char seq, str) =
        if Seq.tryHead str = Some '+' then
            "+", Seq.skip 1 str
        else
            "", str

    let phone = str |> Seq.takeWhile Char.IsDigit
    let name = str |> Seq.skipWhile Char.IsDigit

    if name |> Seq.tryHead |> Option.map Char.IsWhiteSpace <> Some true then
        Error <| "no name given"
    else
        let name = name |> Seq.skipWhile Char.IsWhiteSpace

        if Seq.isEmpty phone then
            Error "no phone given"
        else if Seq.isEmpty name then
            Error "no name given"
        else
            let name = name |> Seq.map string |> String.concat ""
            let phone = [ sign; phone ] |> Seq.concat |> Seq.map string |> String.concat ""
            Entry(name, phone) |> Ok
