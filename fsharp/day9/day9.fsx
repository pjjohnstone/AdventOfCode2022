open System.IO

let mutable history = []

let knots = ['1';'2';'3';'4';'5';'6';'7';'8';'9']

let initGrid size =
  let grid = Array2D.create (size * 2) (size * 2) [||]
  let (initY,initX) = (size - 1),(size - 1)
  let startingPieces = Array.append [|'H';'s'|] (List.toArray knots)
  grid[initY,initX] <- startingPieces
  startingPieces |> Array.iter (fun p -> history <- (p,(initY, initX))::history)
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

let makeAdjacent dir headPiece piece grid =
  let (headY, headX) = getIndexOfPiece headPiece grid
  let (pieceY, pieceX) = getIndexOfPiece piece grid
  takePieceFromPos pieceY pieceX piece grid
  match dir with
  | 'D' -> putPieceAtPos (headY - 1) headX piece grid
  | 'U' -> putPieceAtPos (headY + 1) headX piece grid
  | 'L' -> putPieceAtPos headY (headX + 1) piece grid
  | _ -> putPieceAtPos headY (headX - 1) piece grid

let makeTailAdjacent dir grid =
  makeAdjacent dir 'H' 'T' grid

let runInstruction grid (dir, num) =
  for _ = 1 to num do
    movePiece 'H' dir grid
    if not (isAdjacent 'H' knots[0] grid) then
      makeAdjacent dir 'H' knots[0] grid
    for i = 1 to (knots.Length - 1) do
      if not (isAdjacent knots[i - 1] knots[i] grid) then
        makeAdjacent dir knots[i - 1] knots[i] grid

let maxInDir (instructions: (char * int) list) dir =
  instructions
  |> List.filter (fun (d,_) -> d = dir)
  |> List.map (fun (_,n) -> n)
  |> List.sum

let maxPossibleSize instructions =
  let sizes = [(maxInDir instructions 'U'); (maxInDir instructions 'L'); (maxInDir instructions 'R'); (maxInDir instructions 'D')]
  sizes |> List.max

let lines = File.ReadAllLines "fsharp/day9/test2.txt" |> Array.toList

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

history |> List.filter (fun (p,_) -> p = knots[knots.Length - 1]) |> List.distinct |> List.length
