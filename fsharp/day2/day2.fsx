open System.IO

let lines = File.ReadAllLines("fsharp/day2/input.txt") |> Array.toList

type Shape = {
  Name: string
  Value: int
  Beats: string
}

let (opMove,yourMove) =
  lines
  |> List.map (fun (line: string) -> line.Remove(1,1))
  |> List.map (fun line -> Seq.toList line |> List.splitAt 1)
  |> List.map (fun (f,s) -> (List.exactlyOne f, List.exactlyOne s))
  |> List.unzip

printfn "Opponent's moves: %A" opMove
printfn "Your moves: %A" yourMove