module Hw4.Task3.Tests.Phonebook

open NUnit.Framework
open Hw4.Task3.Phonebook
open FsUnit
open FsCheck

[<Test>]
let TestAdd () =
    Check.QuickThrowOnFailure(fun x l -> add x l = x :: l)

[<Test>]
let TestEmpty () = empty |> should equal ([]: Phonebook)

let findPhonesTestCases =
    [ [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Joe", "+789") ], "Amy", [ "+1234" ]
      [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Joe", "+789") ], "Mark", []
      [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Joe", "+789") ], "Joe", [ "+5678"; "+789" ] ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("findPhonesTestCases")>]
let FindPhoneOfNameTest book name result =
    findPhoneOfName name book |> should equal result

let findNamesTestCases =
    [ [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Joe", "+789") ], "+1234", [ "Amy" ]
      [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Joe", "+789") ], "+234", []
      [ Entry("Joe", "+5678"); Entry("Amy", "+1234"); Entry("Mark", "+5678") ], "+5678", [ "Joe"; "Mark" ] ]
    |> List.map (fun (a, b, c) -> TestCaseData(a, b, c))

[<TestCaseSource("findNamesTestCases")>]
let FindNameOfPhoneTest book phone result =
    findNameOfPhone phone book |> should equal result

let toStringTestCases =
    [ Entry("Joe", "+5678"), "+5678 Joe"
      Entry("Amy", "+1234"), "+1234 Amy"
      Entry("Joe", "+789"), "+789 Joe" ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("toStringTestCases")>]
let ToStringTest entry result = toString entry |> should equal result

let fromStringTestCases =
    [ "+5678 Joe", Entry("Joe", "+5678") |> Ok
      "+1234 Amy", Entry("Amy", "+1234") |> Ok
      "+789 Joe", Entry("Joe", "+789") |> Ok
      "+313", Error "no name given"
      " asd", Error "no phone given" ]
    |> List.map (fun (a, b) -> TestCaseData(a, b))

[<TestCaseSource("fromStringTestCases")>]
let FromStringTest entry result = fromString entry |> should equal result
