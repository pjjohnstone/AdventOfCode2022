open System.IO

let lines = File.ReadAllLines("fsharp/day3/input.txt") |> Array.toList

let dividePartitions (str: string) =
  str
  |> Seq.splitInto 2
  |> Seq.toList

let getBackpacks lines =
  lines
  |> List.map dividePartitions

let existsInArray ch array =
  if Array.contains ch array then Some(ch) else None

let existsInArrays ch array1 array2 =
  if Array.contains ch array1 && Array.contains ch array2
  then Some(ch)
  else None

let getDuplicateItems (partitionOne: char[]) (partitionTwo: char[]) =
  let partTwoDupes =
    partitionOne |> Array.choose (fun item -> existsInArray item partitionTwo)
  let partOneDupes =
    partitionTwo |> Array.choose (fun item -> existsInArray item partitionOne)
  Array.concat [partOneDupes; partTwoDupes]

let getDuplicates (backpacks: char[] list list) =
  backpacks
  |> List.map (fun bp -> getDuplicateItems (List.head bp) (List.last bp))

let scoreItem (char: char) =
  match (System.Char.IsUpper(char)) with
  | false -> (char |> int32) - 96
  | true -> (char |> int32) - 38

let getGroups (lines: string list) =
  let rec getGroupsRec lines (groups: string list list) =
    match lines with
    | [] -> groups
    | _ ->
      getGroupsRec (List.removeManyAt 0 3 lines) ((List.take 3 lines)::groups)
  getGroupsRec lines []

let findBadgeInBackpacks (backpacks: char[] list) =
  backpacks[0]
  |> Array.pick (fun item -> existsInArrays item backpacks[1] backpacks[2])

let getBadge (group: string list) =
  group
  |> List.map (fun str -> str |> Seq.toArray)
  |> findBadgeInBackpacks

let backpacks = getBackpacks lines
let duplicates = getDuplicates backpacks
let score =
  duplicates
  |> List.map (fun a -> Array.head a)
  |> List.map scoreItem
  |> List.sum
let groups = getGroups lines
let badgePriorities =
  groups
  |> List.map getBadge
  |> List.map scoreItem
  |> List.sum
