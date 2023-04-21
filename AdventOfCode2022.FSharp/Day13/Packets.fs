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

