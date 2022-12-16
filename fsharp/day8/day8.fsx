open System.IO

let lines = File.ReadAllLines "fsharp/day8/test.txt"

let numbers =
  lines
  |> Array.map (fun l -> l |> Seq.toArray)
  |> Array.map (fun a ->
    a |> Array.map (fun c -> (c |> int) - 48))

let grid = Array2D.init lines.Length lines[0].Length (fun x y -> numbers[x][y])
