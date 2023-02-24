open System.IO

type Instruction = {
  Value: int
  CyclesRemain: int
  Label: string
}

let parseInstruction (ins: string) =
  ins
  |> Seq.toArray
  |> fun a -> Array.splitAt (Array.findIndex (fun c -> c = ' ') a) a
  |> fun (i,v) ->
    match (System.String.Concat i) with
    | "addx" -> { Label = "addx"; Value = (v |> System.String |> int); CyclesRemain = 2}
    | _ -> { Label = "noop"; Value = 0; CyclesRemain = 1}

let lines = File.ReadAllLines("fsharp/day10/test.txt") |> Array.toList
let buffer = lines |> List.map parseInstruction
