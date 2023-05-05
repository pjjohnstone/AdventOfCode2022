module Packets

type Result = {
  Pair: char * char
  InOrder: bool option
}

let numberComparison (pair: char * char) =
  let (left,right) = pair
  match System.Int32.TryParse (left |> System.String.Concat) with
  | true,intLeft ->
    match System.Int32.TryParse (right |> System.String.Concat) with
    | true,intRight ->
      match intLeft <= intRight with
      | true ->
        { Pair = (left,right); InOrder = Some(true) }
      | false ->
        { Pair = (left,right); InOrder = Some(false) }
    | false,_ ->
      { Pair = (left,right); InOrder = None }
  | false,_ ->
    { Pair = (left,right); InOrder = None }

let evaluateRules pair =
  pair
  |> numberComparison

let inOrder (pair: string * string) =
  let rec inOrderRec (pair: char list * char list) =
    let (left,right) = pair
    match (left.Head = right.Head) with
    | true -> 
      inOrderRec (left.Tail, right.Tail)
    | false ->
      match (evaluateRules (left.Head, right.Head)).InOrder with
      | Some(true) -> true
      | Some(false) -> false
      | _ ->
        inOrderRec (left.Tail, right.Tail)
  let (left,right) = pair
  inOrderRec (left |> Seq.toArray |> Array.toList, right |> Seq.toArray |> Array.toList)