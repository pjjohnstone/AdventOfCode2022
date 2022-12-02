open System.IO

let lines = File.ReadAllLines("fsharp/day2/input.txt") |> Array.toList

type Shape = {
  Name: string
  Value: int
  Beats: string
}

let (opMove, yourMove) =
  lines
  |> List.map (fun (line: string) -> line.Remove(1,1))
  |> List.map (fun line -> Seq.toList line |> List.splitAt 1)
  |> List.map (fun (f,s) -> (List.exactlyOne f, List.exactlyOne s))
  |> List.unzip

let makeShape char =
  match char with
  | 'A' | 'X' -> { Name = "Rock"; Value = 1; Beats = "Scissors" }
  | 'B' | 'Y' -> { Name = "Paper"; Value = 2; Beats = "Rock" }
  | _ -> { Name = "Scissors"; Value = 3; Beats = "Paper" }

let (opShapes, yourShapes) =
  (opMove, yourMove)
  |> fun (f,s) -> (f |> List.map makeShape, s |> List.map makeShape)

let scoreRound opShape yourShape =
  match (opShape.Name = yourShape.Name) with
  | true -> 3 + yourShape.Value
  | false ->
    match (opShape.Name = yourShape.Beats) with
    | true -> 6 + yourShape.Value
    | false -> 0 + yourShape.Value

let finalScore = List.fold2 (fun acc o y -> acc + scoreRound o y) 0 opShapes yourShapes

printfn "Your final score is: %i" finalScore