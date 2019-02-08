// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open Person

let rec factorialCPS x continuation =
    if x <= 1 then continuation()
    else factorialCPS (x - 1) (fun () -> x * continuation())

[<EntryPoint>]
let main argv = 
    let person = { FirstName = "Tommy"; LastName = "Barone"; Age = 22 }
    Console.WriteLine(sprintf "%A" person)
    // person.Age <- 66 => ERROR!
    let newPerson = { person with FirstName = "Jewels" }
    Console.WriteLine(sprintf "%A" person)
    Console.WriteLine(sprintf "%A" newPerson)

    let mutable badPerson = { FirstName = "Evil"; LastName = "Bastard"; Age = 666 }
    Console.WriteLine(sprintf "%A" badPerson)
    badPerson <- { badPerson with FirstName = "Superevil" }
    Console.WriteLine(sprintf "%A" badPerson)


    Console.WriteLine(sprintf "Continuation factorial with 4: %d" (factorialCPS 4 (fun () -> 1)))
    0 // return an integer exit code
