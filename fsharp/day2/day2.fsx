open System.IO

let lines = File.ReadAllLines("fsharp/day2/input.txt") |> Array.toList

type Shape = {
  Name: string
  Value: int
  Beats: string
  Loses: string
}

let (opMove, yourMove) =
  lines
  |> List.map (fun (line: string) -> line.Remove(1,1))
  |> List.map (fun line -> Seq.toList line |> List.splitAt 1)
  |> List.map (fun (f,s) -> (List.exactlyOne f, List.exactlyOne s))
  |> List.unzip

let makeShape char =
  match char with
  | 'A' | 'X' -> { Name = "Rock"; Value = 1; Beats = "Scissors"; Loses = "Paper" }
  | 'B' | 'Y' -> { Name = "Paper"; Value = 2; Beats = "Rock"; Loses = "Scissors" }
  | _ -> { Name = "Scissors"; Value = 3; Beats = "Paper"; Loses = "Rock" }

let getShapeByName name =
  match name with
  | "Rock" -> makeShape 'A'
  | "Paper" -> makeShape 'B'
  | _ -> makeShape 'C'

let chooseResponse opShape desiredOutcome =
  match desiredOutcome with
  | 'X' -> getShapeByName opShape.Beats
  | 'Y' -> getShapeByName opShape.Name
  | _ -> getShapeByName opShape.Loses

let opShapes = List.map makeShape opMove
let yourShapes = List.map2 chooseResponse opShapes yourMove

let scoreRound opShape yourShape =
  match (opShape.Name = yourShape.Name) with
  | true -> 3 + yourShape.Value
  | false ->
    match (opShape.Name = yourShape.Beats) with
    | true -> 6 + yourShape.Value
    | false -> 0 + yourShape.Value

let finalScore = List.fold2 (fun acc o y -> acc + scoreRound o y) 0 opShapes yourShapes

printfn "Final score is: %i" finalScore