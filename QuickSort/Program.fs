// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Diagnostics
open System.Threading.Tasks

let rec quickSortSequential aList =
    match aList with
    | [] -> []
    | first :: rest ->
        let smaller, larger =
            List.partition (fun n -> n < first) rest
        quickSortSequential smaller @ (first :: quickSortSequential larger)

let rec quickSortParallel aList =
    match aList with
    | [] -> []
    | first :: rest ->
        let smaller, larger =
            List.partition (fun n -> n < first) rest
        let left = Task.Run(fun () -> quickSortParallel smaller)
        let right = Task.Run(fun () -> quickSortParallel larger)
        left.Result @ (first :: right.Result)

let rec quickSortParallelWithDepth depth aList =
    match aList with
    | [] -> []
    | first :: rest ->
        let smaller, larger =
            List.partition (fun n -> n < first) rest
        if depth < 0 then
            let left = quickSortParallelWithDepth depth smaller
            let right = quickSortParallelWithDepth depth larger
            left @ (first :: right)
        else
            let left = Task.Run(fun () -> quickSortParallelWithDepth (depth - 1) smaller)
            let right = Task.Run(fun () -> quickSortParallelWithDepth (depth - 1) larger)
            left.Result @ (first :: right.Result)

let getRandomArray() =
    let rand = Random()
    List.init 1000000 (fun __ -> rand.Next())

[<EntryPoint>]
let main _ = 
    let stopWatch = Stopwatch.StartNew()
    let aList = getRandomArray()
    let pCount = float Environment.ProcessorCount
    let logBase = 2.
    let minDepth = 4
    let maxDepth = (((int) (Math.Log(pCount, logBase)) + minDepth))
    let sorted = aList |> quickSortParallelWithDepth maxDepth // quickSortParallel // quickSortSequential
    stopWatch.Stop |> ignore
    Console.WriteLine("{0} elements : {1}", sorted.Length, (float) stopWatch.ElapsedMilliseconds / 1000.0) |> ignore
    0 // return an integer exit code
