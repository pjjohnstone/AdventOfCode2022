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

let toEndOfBlock chars =
  let index = chars |> List.findIndex (fun c -> c.Equals ']')
  chars 
  |> List.splitAt index
  |> fun (x,_) -> x

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

let pairs (input: string[]) =
   input
   |> Array.toList
   |> List.chunkBySize 2

let IsInOrder (input: string[]) = 
  [|true|]

