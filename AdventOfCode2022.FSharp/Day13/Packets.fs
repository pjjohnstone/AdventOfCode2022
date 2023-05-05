module Packets

type Result = {
  Pair: char list * char list
  InOrder: bool option
}

let areSame (pair: char list * char list) =
  let rec areSameRec (pair: char list * char list) =
    let (left,right) = pair
    match left.Head = right.Head with
    | false ->
      { Pair = pair; InOrder = None }
    | true ->
      areSameRec (left.Tail, right.Tail)    
  areSameRec pair

let numberComparison result =
  match result.InOrder with
  | Some(_) -> result
  | None ->
    let (left,right) = result.Pair
    match System.Int32.TryParse (left.Head |> System.String.Concat) with
    | true,intLeft ->
      match System.Int32.TryParse (right.Head |> System.String.Concat) with
      | true,intRight ->
        match intLeft <= intRight with
        | true ->
          { Pair = (left,right); InOrder = Some(true) } // both numbers left smaller
        | false ->
          { Pair = (left,right); InOrder = Some(false) } // both numbers left bigger
      | false,_ ->
        { Pair = (left,right); InOrder = None } // right is not a number
    | false,_ ->
      match System.Char.IsNumber right.Head with
      | true ->
        { Pair = (left,right); InOrder = None } // left is not a number
      | false ->
        { Pair = (left,right); InOrder = None } // neither are numbers

let findNextNumber result =
  let rec findNextNumberRec result =
    let (left,right) = result.Pair
    match System.Char.IsNumber left.Head with
    | true ->
      match System.Char.IsNumber right.Head with
      | true ->
        numberComparison result
      | false ->
        findNextNumberRec { Pair = (left, right.Tail); InOrder = None }
    | false ->
      findNextNumberRec { Pair = (left.Tail, right); InOrder = None }
  match result.InOrder with
  | Some(_) -> result
  | None ->
    findNextNumberRec result

let evaluateRules pair =
  pair
  |> areSame
  |> numberComparison
  |> findNextNumber

let inOrder (pair: string * string) =
  let rec inOrderRec (pair: string * string) =
    let (left,right) = pair
    let charPairs = (left |> Seq.toArray |> Array.toList, right |> Seq.toArray |> Array.toList)
    let result = evaluateRules charPairs
    match result.InOrder with
    | Some(true) -> true
    | Some(false) -> false
    | None -> inOrderRec (left[1..], right[1..])
  inOrderRec pair
  