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
  printfn "Moving %c to %i,%i" piece y x
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

let isAdjacent headPiece tailPiece grid =
  let (tailPieceY, tailPieceX) = getIndexOfPiece tailPiece grid
  let (headY, headX) = getIndexOfPiece headPiece grid
  if headY - tailPieceY > 1 || tailPieceY - headY > 1 || headX - tailPieceX > 1 || tailPieceX - headX > 1 then false
  else true

let isTailAdjacent grid =
  isAdjacent 'H' 'T' grid

let makeAdjacent dir piece grid =
  let (headY, headX) = getIndexOfPiece 'H' grid
  let (pieceY, pieceX) = getIndexOfPiece piece grid
  takePieceFromPos pieceY pieceX 'T' grid
  match dir with
  | 'D' -> putPieceAtPos (headY - 1) headX 'T' grid
  | 'U' -> putPieceAtPos (headY + 1) headX 'T' grid
  | 'L' -> putPieceAtPos headY (headX + 1) 'T' grid
  | _ -> putPieceAtPos headY (headX - 1) 'T' grid

let makeTailAdjacent dir grid =
  makeAdjacent dir 'T' grid

let runInstruction grid (dir, num) =
  for _ = 1 to num do
    movePiece 'H' dir grid
    if not (isTailAdjacent grid) then
      makeTailAdjacent dir grid

let maxInDir (instructions: (char * int) list) dir =
  instructions
  |> List.filter (fun (d,_) -> d = dir)
  |> List.map (fun (_,n) -> n)
  |> List.sum

let maxPossibleSize instructions =
  let sizes = [(maxInDir instructions 'U'); (maxInDir instructions 'L'); (maxInDir instructions 'R'); (maxInDir instructions 'D')]
  sizes |> List.max

let lines = File.ReadAllLines "fsharp/day9/test.txt" |> Array.toList

let instructions =
  lines
  |> List.map (fun s -> s.Split " ")
  |> List.map (fun a ->
    a
    |> Array.pairwise
    |> Array.exactlyOne)
  |> List.map (fun (d,n) -> ((d |> Seq.toArray |> Array.exactlyOne), int n))

printfn "Grid size will be: %i" (maxPossibleSize instructions)

let grid = initGrid (maxPossibleSize instructions)

instructions |> List.iter (fun i -> runInstruction grid i)

history |> List.filter (fun (p,_) -> p = 'T') |> List.distinct |> List.length