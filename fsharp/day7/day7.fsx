open System.IO

type Directory = {
  Name: string
  Files: string list
  Children: Directory list
}

let isCommand (line: string) =
  if line.StartsWith '$' then true else false

let chunkInput (lines: string list) =
  let directoryName = lines.Head |> Seq.toArray |> Array.removeManyAt 0 5 |> System.String
  lines
  |> List.removeManyAt 0 2
  |> List.takeWhile (fun line -> not(isCommand line))
  |> fun contents ->
    (directoryName, contents)

let lines = File.ReadAllLines "fsharp/day7/test.txt" |> Array.toList

let dir = chunkInput lines