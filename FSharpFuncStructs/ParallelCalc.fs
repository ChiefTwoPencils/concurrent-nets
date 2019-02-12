module ParallelCalc

open System.Threading.Tasks
open System

type Operation = Add | Sub | Mul | Div | Pow

type Caculator =
    | Value of double
    | Expr of Operation * Caculator * Caculator

let spawn (op: unit -> double) = Task.Run(op)

let rec eval expr =
    match expr with
    | Value(value) -> value
    | Expr(op, lExpr, rExpr) ->
        let op1 = spawn(fun () -> eval lExpr)
        let op2 = spawn(fun () -> eval rExpr)
        let apply = Task.WhenAll([op1; op2])
        let lRes, rRes = apply.Result.[0], apply.Result.[1]
        match op with
        | Add -> lRes + rRes
        | Sub -> lRes - rRes
        | Mul -> lRes * rRes
        | Div -> lRes / rRes
        | Pow -> Math.Pow(lRes, rRes)