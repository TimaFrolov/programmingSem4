module Hw4.Task2.Tests

open NUnit.Framework
open FsCheck

[<Test>]
let funcIsSameAsFunc2 () =
    Check.QuickThrowOnFailure(fun x l -> func x l = func2 x l)
