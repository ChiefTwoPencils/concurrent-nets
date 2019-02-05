module Person

type Person = { FirstName:string; LastName:string; Age:int }
let person = { FirstName = "Tommy"; LastName = "Barone"; Age = 22 }
// person.Age <- 66 => ERROR!
let newPerson = { person with FirstName = "Jewels" }

let mutable badPerson = { FirstName = "Evil"; LastName = "Bastard"; Age = 666 }
badPerson = { badPerson with FirstName = "Superevil" } |> ignore