open System.IO

let initGrid size =
  let grid = Array2D.create size size [||]
  grid[(size - 1),0] <- [|'H'; 'T'; 's'|]
  grid

let isPieceAtPos y x piece (grid: char[][,]) =
  if (Array.exists (fun e -> e = piece) grid[y,x]) then true else false

let getIndexOfPiece piece (grid: char[][,]) =
  let mutable result = (0,0)
  for i in 0..(Array2D.length1 grid - 1) do
    for j in 0..(Array2D.length1 grid - 1) do
      if isPieceAtPos i j piece grid then result <- (i,j)
  result

let movePiece piece y x (grid: char[][,]) =
  let (currentY, currentX) = getIndexOfPiece piece grid
  grid[currentY,currentX] <- grid[currentY,currentX]
  |> Array.toList
  |> List.filter (fun e -> e <> piece)
  |> List.toArray
  grid[y,x] <- Array.insertAt 0 piece grid[y,x]

let lines = File.ReadAllLines "fsharp/day9/test.txt" |> Array.toList

let instructions =
  lines
  |> List.map (fun s -> s.Split " ")
  |> List.map (fun a ->
    a
    |> Array.pairwise
    |> Array.exactlyOne)
  |> List.map (fun (d,n) -> (d, int n))

let grid = initGrid 6

getIndexOfPiece 'H' grid
movePiece 'H' 5 1 grid
