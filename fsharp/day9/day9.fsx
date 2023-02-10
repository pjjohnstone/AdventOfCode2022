open System.IO

let mutable history = []

let initGrid size =
  let grid = Array2D.create (size * 2) (size * 2) [||]
  let (initY,initX) = (size - 1),(size - 1)
  grid[initY,initX] <- [|'H'; 'T'; 's'|]
  history <- ('H',(initY, initX))::history
  history <- ('T',(initY, initX))::history
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
  printfn "Putting %c at %i,%i" piece y x
  grid[y,x] <- Array.insertAt 0 piece grid[y,x]
  history <- (piece,(y,x))::history

let movePiece piece direction (grid: char[][,]) =
  let (currentY, currentX) = getIndexOfPiece piece grid
  takePieceFromPos currentY currentX piece grid
  match direction with
  | 'D' -> putPieceAtPos (currentY + 1) currentX piece grid
  | 'U' -> putPieceAtPos (currentY - 1) currentX piece grid
  | 'L' -> putPieceAtPos currentY (currentX - 1) piece grid
  | _ -> putPieceAtPos currentY (currentX + 1) piece grid

let isTailAdjacent grid =
  let (tailY, tailX) = getIndexOfPiece 'T' grid
  let (headY, headX) = getIndexOfPiece 'H' grid
  if headY - tailY > 1 || tailY - headY > 1 || headX - tailX > 1 || tailX - headX > 1 then false
  else true

let makeTailAdjacent dir grid =
  let (currentY, currentX) = getIndexOfPiece 'H' grid
  let (tailY, tailX) = getIndexOfPiece 'T' grid
  takePieceFromPos tailY tailX 'T' grid
  match dir with
  | 'D' -> putPieceAtPos (currentY - 1) currentX 'T' grid
  | 'U' -> putPieceAtPos (currentY + 1) currentX 'T' grid
  | 'L' -> putPieceAtPos currentY (currentX + 1) 'T' grid
  | _ -> putPieceAtPos currentY (currentX - 1) 'T' grid

let runInstruction grid (dir, num) =
  for _ = 1 to num do
    movePiece 'H' dir grid
    if not (isTailAdjacent grid) then
      makeTailAdjacent dir grid

let lines = File.ReadAllLines "fsharp/day9/input.txt" |> Array.toList

let instructions =
  lines
  |> List.map (fun s -> s.Split " ")
  |> List.map (fun a ->
    a
    |> Array.pairwise
    |> Array.exactlyOne)
  |> List.map (fun (d,n) -> ((d |> Seq.toArray |> Array.exactlyOne), int n))

let size = (instructions |> List.map (fun (_,i) -> i) |> List.max) * 30

let grid = initGrid size

instructions |> List.iter (fun i -> runInstruction grid i)

history |> List.filter (fun (p,_) -> p = 'T') |> List.distinct |> List.length