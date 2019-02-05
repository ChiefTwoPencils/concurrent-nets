module FList

type FList<'a> =
    | Empty
    | Cons of head: 'a * tail: FList<'a>

let rec map f (list: FList<'a>) =
    match list with
    | Empty -> Empty
    | Cons(head, tail) -> Cons(f head, map f tail)

let rec filter p (list: FList<'a>) =
    match list with
    | Empty -> Empty
    | Cons(head, tail) when p head -> Cons(head, filter p tail)
    | Cons(_, tail) -> filter p tail


