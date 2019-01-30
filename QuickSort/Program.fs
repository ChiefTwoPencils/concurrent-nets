// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Diagnostics

let rec quickSortSequential aList =
    match aList with
    | [] -> []
    | first :: rest ->
        let smaller, larger =
            List.partition (fun n -> n < first) rest
        quickSortSequential smaller @ (first :: quickSortSequential larger)

let getRandomArray() =
    let rand = Random()
    List.init 1000000 (fun __ -> rand.Next())

[<EntryPoint>]
let main _ = 
    let stopWatch = Stopwatch.StartNew()
    let aList = getRandomArray()
    let sorted = aList |> quickSortSequential
    stopWatch.Stop |> ignore
    Console.WriteLine("{0} elements : {1}", sorted.Length, (float) stopWatch.ElapsedMilliseconds / 1000.0) |> ignore
    0 // return an integer exit code
