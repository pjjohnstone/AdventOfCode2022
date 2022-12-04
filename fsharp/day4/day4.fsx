open System.IO

let lines = File.ReadAllLines("fsharp/day4/input.txt") |> Array.toList

let splitLine line =
  line
  |> Seq.findIndex (fun c -> c = ',')
  |> fun i -> line |> Seq.toList |> List.splitAt i

let charsToInt (chars: char list) =
  chars
  |> List.toArray
  |> System.String
  |> int

let removeSymbols charList =
  charList
  |> List.filter (fun c -> System.Char.IsNumber(c))

let convertAssignment assgn =
  let index = List.findIndex (fun c -> c = '-') assgn
  let firstBatch =
    assgn |> List.take index |> removeSymbols
  let secondBatch =
    assgn |> List.removeManyAt 0 index |> removeSymbols
  [(charsToInt firstBatch); (charsToInt secondBatch)]

let convertAssignmentPairs (first,second) =
  (convertAssignment first, convertAssignment second)

let getRanges (first: int list, second: int list) =
  ([first.Head..(List.last first)],[second.Head..(List.last second)])

let getPairs lines =
  lines
  |> List.map splitLine
  |> List.map convertAssignmentPairs
  |> List.map getRanges

let fullContained (first, second) =
  first |> List.forall (fun e -> List.contains e second)
  ||
  second |> List.forall (fun e -> List.contains e first)

let pairs = getPairs lines
let fullyOverlapped =
  pairs |> List.filter fullContained |> List.length
