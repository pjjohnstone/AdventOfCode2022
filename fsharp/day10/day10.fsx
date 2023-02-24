open System.IO

type Instruction = {
  Value: int
  CyclesRemain: int
  Label: string
}

type RegisterState = {
  X: int
  Instructions: Instruction list
}

let parseInstruction (ins: string) =
  match ins with
  | "noop" -> { Label = "noop"; Value = 0; CyclesRemain = 1}
  | _ ->
    ins
    |> Seq.toArray
    |> fun a -> Array.splitAt (Array.findIndex (fun c -> c = ' ') a) a
    |> fun (i,v) ->
      { Label = "addx"; Value = (v |> System.String |> int); CyclesRemain = 2}

let runInstruction instruction =
  let newInstruction = { instruction with CyclesRemain = (instruction.CyclesRemain - 1)}
  match newInstruction.CyclesRemain with
  | 0 -> (Some(newInstruction.Value), None)
  | _ -> (None, Some(newInstruction))

let processRegister buffer =
  let rec processRegisterRec buffer register history cycle =
    match buffer with
    | [] -> history
    | _ ->
      let processedInstructions =
        register.Instructions |> List.map (fun i -> runInstruction i)
      let newXValue =
        processedInstructions
        |> List.map (fun (x,_) -> x)
        |> List.choose id
        |> List.sum
      let newInstructions = (processedInstructions |> List.map (fun (_,i) -> i) |> List.choose id)@([buffer.Head])
      processRegisterRec buffer.Tail { X = newXValue; Instructions = newInstructions} ((cycle, register)::history) (cycle + 1)
  processRegisterRec buffer { X = 0; Instructions = []} [] 0

let getValueAtCycle (history: (int * RegisterState) list) cycle =
  history
  |> List.filter (fun (c,_) -> c = cycle)
  |> List.exactlyOne
  |> fun (_,r) -> r.X

let lines = File.ReadAllLines("fsharp/day10/test.txt") |> Array.toList
let buffer = lines |> List.map parseInstruction
let history = processRegister buffer

printfn "Value of X at cycle %i was %i" 20 (getValueAtCycle history 20)
printfn "Value of X at cycle %i was %i" 60 (getValueAtCycle history 60)
printfn "Value of X at cycle %i was %i" 100 (getValueAtCycle history 100)
printfn "Value of X at cycle %i was %i" 140 (getValueAtCycle history 140)
printfn "Value of X at cycle %i was %i" 180 (getValueAtCycle history 180)
printfn "Value of X at cycle %i was %i" 220 (getValueAtCycle history 220)