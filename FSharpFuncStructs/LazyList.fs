module LazyList

type LazyList<'a> =
    | Cons of head: 'a * tail: Lazy<'a LazyList>
    | Empty

let rec append items list =
    match items with
    | Cons(head, Lazy(tail)) ->
        Cons(head, lazy(append tail list))
    | Empty -> list

let rec iter action list =
    match list with
    | Cons(head, Lazy(tail)) ->
        action head
        iter action tail
    | Empty -> ()