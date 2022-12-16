open System.IO

let flatten (A:'a[,]) = A |> Seq.cast<'a>

let getColumn c (A:_[,]) =
  flatten A.[*,c..c] |> Seq.toArray

let getRow r (A:_[,]) =
  flatten A.[r..r,*] |> Seq.toArray

let isOnEdge x y grid =
  if x = 0 || y = 0 || x = ((getRow x grid).Length - 1) || y = ((getColumn y grid).Length - 1)
  then true
  else false

let visibleFromLeft x y (grid: int[,]) =
  if ((getRow x grid) |> Array.take y |> Array.max) < grid[x,y] then true else false

let visibleFromRight x y (grid: int[,]) =
  if ((getRow x grid) |> Array.removeManyAt 0 (y + 1) |> Array.max) < grid[x,y] then true else false

let visibleFromTop x y (grid: int[,]) =
  if ((getColumn y grid) |> Array.take x |> Array.max) < grid[x,y] then true else false

let visibleFromBottom x y (grid: int[,]) =
  if ((getColumn y grid) |> Array.removeManyAt 0 (x + 1) |> Array.max) < grid[x,y] then true else false

let visible x y (grid: int[,]) =
  if (isOnEdge x y grid) || (visibleFromLeft x y grid) || (visibleFromRight x y grid) || (visibleFromTop x y grid) || (visibleFromBottom x y grid)
  then true
  else false

let scenicScore num (array: int[]) =
  if array.Length = 0 then 0
  else
    let treesUntilBlocked = array |> Array.takeWhile (fun e -> num > e)
    if treesUntilBlocked.Length = array.Length then treesUntilBlocked.Length
    else treesUntilBlocked.Length + 1

let scenicLeft x y (grid: int[,]) =
  (getRow x grid) |> Array.take y |> Array.rev |> scenicScore grid[x,y]

let scenicRight x y (grid: int[,]) =
  (getRow x grid) |> Array.removeManyAt 0 (y + 1) |> scenicScore grid[x,y]

let scenicTop x y (grid: int[,]) =
  (getColumn y grid) |> Array.take x |> Array.rev |> scenicScore grid[x,y]

let scenicBottom x y (grid: int[,]) =
  (getColumn y grid) |> Array.removeManyAt 0 (x + 1) |> scenicScore grid[x,y]

let totalScenic x y grid =
  (scenicLeft x y grid) * (scenicRight x y grid) * (scenicTop x y grid) * (scenicBottom x y grid)

let lines = File.ReadAllLines "fsharp/day8/input.txt"

let numbers =
  lines
  |> Array.map (fun l -> l |> Seq.toArray)
  |> Array.map (fun a ->
    a |> Array.map (fun c -> (c |> int) - 48))

let grid = Array2D.init lines.Length lines[0].Length (fun x y -> numbers[x][y])

let mutable numVisible = 0

grid |> Array2D.iteri (fun x y _ -> if visible x y grid then numVisible <- (numVisible + 1))

let mutable highestScenicScore = 0

grid |> Array2D.iteri (fun x y _ -> if (totalScenic x y grid) > highestScenicScore then highestScenicScore <- (totalScenic x y grid))
