open System.IO

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
