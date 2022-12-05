open System.IO

type Instruction = {
  NumberToMove: int
  SourceStack: int
  DestinationStack: int
}

let charToInt (c: char) =
  int c - int '0'

let parseInstruction (nums: int list) =
  { NumberToMove = nums[0]; SourceStack = nums[1] - 1; DestinationStack = nums[2] - 1 }

let splitInstruction (str: string) =
  let fIndex = str |> Seq.toArray |> Array.findIndex (fun c -> c = 'f')
  let (first, rest) = str |> Seq.toArray |> Array.splitAt fIndex
  let tIndex = rest |> Seq.toArray |> Array.findIndex (fun c -> c = 't')
  let (second, third) = rest |> Array.splitAt tIndex
  [first; second; third]
  |> List.map (fun arr -> Array.filter (fun c -> System.Char.IsNumber c) arr)
  |> List.map (fun arr -> arr |> System.String |> int)

let parseInstructions (inst: string list) =
  inst
  |> List.map splitInstruction
  |> List.map parseInstruction

let getCrates (cratesAndNums: string list) =
  cratesAndNums
  |> List.map Seq.toArray
  |> List.removeAt (List.length cratesAndNums - 1)

let fullRow (row: char[]) (indices: int list) numStacks =
  let newRow = Array.create numStacks ' '
  for i in 0..(numStacks - 1) do
    if indices[i] < row.Length then
      newRow[i] <- row[(indices[i])]
  newRow

let buildRows crates indices numstacks =
  crates
  |> List.map (fun row -> fullRow row indices numstacks)

let trimStack stack =
  stack
  |> List.filter (fun c -> System.Char.IsLetter c)

let rowsToStacks rows =
  let rec rowsToStacksRec (rows: char[] list) (stacks: char list list) =
    match rows with
    | [] -> stacks
    | _ ->
      match (Array.length rows[0] = 0) with
      | true -> stacks
      | false ->
        rows
        |> List.map (fun row -> Array.take 1 row)
        |> List.map Array.exactlyOne
        |> fun stack ->
          rowsToStacksRec (rows |> List.map (fun a -> Array.removeAt 0 a)) (stack::stacks)
  rowsToStacksRec rows [] |> List.rev |> List.map trimStack |> List.map List.toArray |> List.toArray

let applyInstruction (stacks: char[][]) instruction =
  let crates = Array.take instruction.NumberToMove stacks[instruction.SourceStack]
  stacks[instruction.DestinationStack] <- Array.concat [crates; stacks[instruction.DestinationStack]]
  stacks[instruction.SourceStack] <- Array.removeManyAt 0 instruction.NumberToMove stacks[instruction.SourceStack]

let topCrate (stacks: char[][]) =
  stacks
  |> Array.map (Array.take 1)
  |> Array.map Array.exactlyOne
  |> System.String

let usefulIndices = [1;5;9;13;17;21;25;29;33]

let lines = File.ReadAllLines("fsharp/day5/input.txt") |> Array.toList

let (crates, stackNumbers, instructions) =
  lines
  |> List.splitAt (List.findIndex (fun (l: string) -> l.Length < 1) lines)
  |> fun (cr, ins) ->
    ((getCrates cr), (List.last cr), (List.removeAt 0 ins))

let numberOfStacks =
  stackNumbers
  |> Seq.toArray
  |> Array.filter (fun c -> System.Char.IsNumber c)
  |> Array.last
  |> fun c -> charToInt c

let rows = buildRows crates usefulIndices numberOfStacks
let stacks = rowsToStacks rows
let parsedInstructions = parseInstructions instructions

parsedInstructions
|> List.iter (fun i -> applyInstruction stacks i)

let topCrates = topCrate stacks
