open System.IO

let compactRow row =
  row
  |> Seq.filter (fun c -> System.Char.IsLetter c || System.Char.IsNumber c)

let stripSymbols row =
  row
  |> Seq.filter (fun c -> not(c = '[' || c = ']'))

let lines = File.ReadAllLines("fsharp/day5/test.txt") |> Array.toList

let (crates,instructions) =
  lines
  |> List.splitAt (List.findIndex (fun (l: string) -> l.Length < 1) lines)
  |> fun (cr, ins) ->
    ((List.removeAt (List.length cr - 1) cr), (List.removeAt 0 ins))

let stacks =
  crates
  |> List.map (fun row -> row |> Seq.chunkBySize 4 |> Seq.toArray)

printfn "%A" stacks[0]
printfn "%A" stacks[1]
printfn "%A" stacks[2]