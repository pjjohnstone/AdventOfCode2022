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
    |> fun (_,v) ->
      { Label = "addx"; Value = (v |> System.String |> int); CyclesRemain = 2}

let runInstruction instruction =
  match instruction.CyclesRemain with
  | 0 -> (Some(instruction.Value), None)
  | _ -> (None, Some(instruction))

let decrementInstructions instructions =
  instructions
  |> List.map (fun i -> {i with CyclesRemain = i.CyclesRemain - 1})

let executeCycle register =
  let processedInstructions =
    register.Instructions
    |> List.map (fun i -> runInstruction i)
  let newXValue =
    processedInstructions
    |> List.map (fun (x,_) -> x)
    |> List.choose id
    |> List.sum
  let instructions =
    processedInstructions
    |> List.map (fun (_,i) -> i)
    |> List.choose id
    |> decrementInstructions
  { X = register.X + newXValue; Instructions = instructions }

let processRegister buffer =
  let rec processRegisterRec (buffer: Instruction list) register history cycle =
    match buffer with
    | [] ->
      match register.Instructions with
      | [] -> ((cycle, register)::history)
      | _ ->
        processRegisterRec [] (executeCycle register) ((cycle, register)::history) (cycle + 1)
    | _ ->
      processRegisterRec buffer.Tail (executeCycle { register with Instructions = register.Instructions@[buffer.Head] }) ((cycle, register)::history) (cycle + 1)
  processRegisterRec buffer { X = 1; Instructions = []} [] 0

let prettyPrintRegisterState state =
  let mutable string = ""
  for instruction in state.Instructions do
    string <- string + $" {instruction.Label} {instruction.Value} {instruction.CyclesRemain}"
  printfn "X: %i Instructions: %s" state.X string

let getStateAtCycle (history: (int * RegisterState) list) cycle =
  history
  |> List.filter (fun (c,_) -> c = cycle)
  |> List.exactlyOne
  |> fun (c,s) ->
    printf "Cycle: %i " c
    prettyPrintRegisterState s

let getValueAtCycle (history: (int * RegisterState) list) cycle =
  history
  |> List.filter (fun (c,_) -> c = cycle)
  |> List.exactlyOne
  |> fun (_,r) -> r.X

let lines = File.ReadAllLines("fsharp/day10/test.txt") |> Array.toList
let buffer = lines |> List.map parseInstruction
let history = processRegister buffer

printfn "%A" history
getStateAtCycle history 0
getStateAtCycle history 1
getStateAtCycle history 2
getStateAtCycle history 3
getStateAtCycle history 4
getStateAtCycle history 5
printfn "Value of X at cycle %i was %i" 18 (getValueAtCycle history 18)
printfn "Value of X at cycle %i was %i" 19 (getValueAtCycle history 19)
printfn "Value of X at cycle %i was %i" 20 (getValueAtCycle history 20)
printfn "Value of X at cycle %i was %i" 21 (getValueAtCycle history 21)
printfn "Value of X at cycle %i was %i" 22 (getValueAtCycle history 22)
// printfn "Value of X at cycle %i was %i" 60 (getValueAtCycle history 60)
// printfn "Value of X at cycle %i was %i" 100 (getValueAtCycle history 100)
// printfn "Value of X at cycle %i was %i" 140 (getValueAtCycle history 140)
// printfn "Value of X at cycle %i was %i" 180 (getValueAtCycle history 180)
// printfn "Value of X at cycle %i was %i" 220 (getValueAtCycle history 220)