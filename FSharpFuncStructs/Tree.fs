module Tree

type Tree<'a> =
    | Node of leaf: 'a * left: Tree<'a> * right: Tree<'a>
    | Empty

let rec contains item tree =
    match tree with
    | Node(leaf, left, right) -> 
        if leaf = item then true
        elif leaf < item then contains item left
        else contains item right
    | Empty -> false

let rec insert item tree =
    match tree with
    | Node(leaf, left, right) as node ->
        if leaf = item then node
        elif leaf < item then Node(leaf, insert item left, right)
        else Node(leaf, left, insert item right)
    | Empty -> Node(item, Empty, Empty)

let rec preorder action tree =
    match tree with
    | Node(leaf, left, right) ->
        action leaf
        preorder action left
        preorder action right
    | Empty -> ()

let rec inorder action tree =
    match tree with
    | Node(leaf, left, right) ->
        inorder action left
        action leaf
        inorder action right
    | Empty -> ()

let rec postorder action tree =
    match tree with
    | Node(leaf, left, right) ->
        postorder action left 
        postorder action right
        action leaf
    | Empty -> ()



