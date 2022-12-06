open System.IO

let lines =
  File.ReadAllLines "fsharp/day6/input.txt"
  |> Array.toList
  |> List.map Seq.toArray

let isMarker letters =
  if (Array.distinct letters).Length = letters.Length then true else false

let findMarker (line: char[]) windowSize =
  let rec findMarkerRec (line: char[]) index =
    match line with
    | [||] -> index + windowSize
    | _ ->
      match (isMarker (Array.take windowSize line)) with
      | true -> index + windowSize
      | false ->
        findMarkerRec (Array.removeAt 0 line) (index + 1)
  findMarkerRec line 0

let firstMarkers = lines |> List.map (fun line -> findMarker line 14)
