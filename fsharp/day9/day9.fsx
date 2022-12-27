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

let takePieceFromPos y x piece (grid: char[][,]) =
  grid[y,x] <- grid[y,x]
  |> Array.toList
  |> List.filter (fun e -> e <> piece)
  |> List.toArray

let putPieceAtPos y x piece (grid: char[][,]) =
  grid[y,x] <- Array.insertAt 0 piece grid[y,x]

let movePiece piece direction (grid: char[][,]) =
  let (currentY, currentX) = getIndexOfPiece piece grid
  takePieceFromPos currentY currentX piece grid
  match direction with
  | 'D' -> putPieceAtPos (currentY + 1) currentX piece grid
  | 'U' -> putPieceAtPos (currentY - 1) currentX piece grid
  | 'L' -> putPieceAtPos currentY (currentX - 1) piece grid
  | _ -> putPieceAtPos currentY (currentX + 1) piece grid
  printfn "%A" grid

let runInstruction grid (dir, num) =
  for _ = 1 to num do
    movePiece 'H' dir grid

let lines = File.ReadAllLines "fsharp/day9/test.txt" |> Array.toList

let instructions =
  lines
  |> List.map (fun s -> s.Split " ")
  |> List.map (fun a ->
    a
    |> Array.pairwise
    |> Array.exactlyOne)
  |> List.map (fun (d,n) -> ((d |> Seq.toArray |> Array.exactlyOne), int n))

let grid = initGrid 6

instructions |> List.iter (fun i -> runInstruction grid i)