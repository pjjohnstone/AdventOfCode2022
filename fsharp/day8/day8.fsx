open System.IO

let flatten (A:'a[,]) = A |> Seq.cast<'a>

let getColumn c (A:_[,]) =
    flatten A.[*,c..c] |> Seq.toArray

let getRow r (A:_[,]) =
    flatten A.[r..r,*] |> Seq.toArray

let visibleFromLeft x y (grid: int[,]) =
  if ((getRow x grid) |> Array.take y |> Array.max) < grid[x,y] then true else false

let visibleFromRight x y (grid: int[,]) =
  if ((getRow x grid) |> Array.removeManyAt 0 (y + 1) |> Array.max) < grid[x,y] then true else false

let visibleFromTop x y (grid: int[,]) =
  if ((getColumn y grid) |> Array.take x |> Array.max) < grid[x,y] then true else false

let visibleFromBottom x y (grid: int[,]) =
  if ((getColumn y grid) |> Array.removeManyAt 0 (x + 1) |> Array.max) < grid[x,y] then true else false

let lines = File.ReadAllLines "fsharp/day8/test.txt"

let numbers =
  lines
  |> Array.map (fun l -> l |> Seq.toArray)
  |> Array.map (fun a ->
    a |> Array.map (fun c -> (c |> int) - 48))

let grid = Array2D.init lines.Length lines[0].Length (fun x y -> numbers[x][y])

let hopefullyTrue = visibleFromBottom 2 0 grid
let hopefullyFalse = visibleFromBottom 2 1 grid