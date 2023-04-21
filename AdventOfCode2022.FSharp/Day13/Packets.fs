module Packets

type Group = {
  Values: int list
  Enclosed: bool
}

type Packet = {
  Groups: Group list
}

let trimList chars =
  chars
  |> List.tail
  |> List.rev
  |> List.tail
  |> List.rev

let toEndOfBlock (symbol: char) chars =
  let index = chars |> List.findIndex (fun c -> c.Equals symbol)
  chars 
  |> List.splitAt index

let intFromCharList (chars: char list) =
  chars
  |> List.toArray
  |> System.String.Concat
  |> int

let groupNumbers (block: char list) =
  let rec groupNumbersRec (block: char list) (numbers: int list) =
    match block with
    | [] -> numbers
    | _ -> 
      match (List.tryFindIndex (fun c -> c.Equals ',') block) with
      | Some(index) ->
        let (number,remains) = List.splitAt index block
        groupNumbersRec remains.Tail (numbers@[(intFromCharList number)])
      | None ->
        groupNumbersRec [] (numbers@[(intFromCharList block)])
  groupNumbersRec block []

let groups (string: string) =
  string
  |> Seq.toArray
  |> Array.toList
  |> trimList

let packets (groupsString: char list) =
  let rec packetsRec (groupsString: char list) groups =
    match groupsString with
    | [] -> { Groups = groups }
    | _ ->
      match groupsString.Head with
      | '[' ->
        let (group,remains) = toEndOfBlock ']' groupsString
        packetsRec remains.Tail (groups@[{ Values = (groupNumbers group.Tail); Enclosed = true }])
      | ',' -> 
        packetsRec groupsString.Tail groups
      | _ ->
        match (List.exists (fun c -> c.Equals ',') groupsString) with
        | true ->
          let (group,remains) = toEndOfBlock ',' groupsString
          packetsRec remains.Tail (groups@[{ Values = (groupNumbers group); Enclosed = false }])
        | false ->
          packetsRec [] (groups@[{ Values = (groupNumbers groupsString); Enclosed = false }])
  packetsRec groupsString []

let pairs (input: string[]) =
   input
   |> Array.toList
   |> List.chunkBySize 2

let IsInOrder (input: string[]) = 
  [|true|]

