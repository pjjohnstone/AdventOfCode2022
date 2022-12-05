open System.IO

type Instruction = {
  NumberToMove: int
  SourceStack: int
  DestinationStack: int
}

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
  rowsToStacksRec rows [] |> List.map List.rev |> List.rev

let usefulIndices = [1;5;9]

let lines = File.ReadAllLines("fsharp/day5/test.txt") |> Array.toList

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
  |> fun c -> int c - int '0'

let rows = buildRows crates usefulIndices numberOfStacks
let stacks = rowsToStacks rows