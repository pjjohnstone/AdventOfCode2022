open System.IO

let lines = File.ReadAllLines("fsharp/day3/input.txt") |> Array.toList

let dividePartitions (str: string) =
  str
  |> Seq.splitInto 2
  |> Seq.toList

let getBackpacks lines =
  lines
  |> List.map dividePartitions

let existsInList ch array =
  if Array.contains ch array then Some(ch) else None

let getDuplicateItems (partitionOne: char[]) (partitionTwo: char[]) =
  let partTwoDupes =
    partitionOne |> Array.choose (fun item -> existsInList item partitionTwo)
  let partOneDupes =
    partitionTwo |> Array.choose (fun item -> existsInList item partitionOne)
  Array.concat [partOneDupes; partTwoDupes]

let getDuplicates (backpacks: char[] list list) =
  backpacks
  |> List.map (fun bp -> getDuplicateItems (List.head bp) (List.last bp))

let scoreItem (char: char) =
  match (System.Char.IsUpper(char)) with
  | false -> (char |> int32) - 96
  | true -> (char |> int32) - 38

let scoreDuplicates (dupes: char[]) =
  dupes
  |> Array.map (fun item -> scoreItem item)
  |> Array.sum

let backpacks = getBackpacks lines
let duplicates = getDuplicates backpacks
let score =
  duplicates
  |> List.map (fun a -> Array.head a)
  |> List.map scoreItem
  |> List.sum
