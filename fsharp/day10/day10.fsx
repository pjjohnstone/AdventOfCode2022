open System.IO

type Instruction = {
  Value: int
  CyclesRemain: int
  Label: string
}

type RegisterState = {
  X: int
  Instruction: Instruction
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

let tick instruction =
  { instruction with CyclesRemain = instruction.CyclesRemain - 1 }

let runInstruction instruction =
  match instruction.CyclesRemain with
  | 0 -> (Some(instruction.Value), None)
  | _ -> (None, Some(instruction))

let executeCycle register (buffer: Instruction list) =
  let (newXOption, newInstOption) = runInstruction register.Instruction
  let newXValue =
    match newXOption with
    | None -> 0
    | Some(x) -> x
  match newInstOption with
  | None ->
    ({ X = register.X + newXValue; Instruction = buffer.Head}, buffer.Tail)
  | Some(i) ->
    ({ X = register.X + newXValue; Instruction = i }, buffer)

let processRegister (buffer: Instruction list) =
  let rec processRegisterRec (buffer: Instruction list) register history cycle =
    let tickedRegister = { register with Instruction = (tick register.Instruction )}
    match buffer with
    | [] ->
      match tickedRegister.Instruction.CyclesRemain with
      | 0 -> ((cycle, tickedRegister)::history)
      | _ ->
        let (newRegState, newBuffer) = executeCycle tickedRegister buffer
        processRegisterRec newBuffer newRegState ((cycle, tickedRegister)::history) (cycle + 1)
    | _ ->
      let (newRegState, newBuffer) = executeCycle tickedRegister buffer
      processRegisterRec newBuffer newRegState ((cycle, tickedRegister)::history) (cycle + 1)
  processRegisterRec buffer.Tail { X = 1; Instruction = buffer.Head} [] 1

let prettyPrintRegisterState state =
  let mutable string = ""
  string <- string + $" {state.Instruction.Label} {state.Instruction.Value} {state.Instruction.CyclesRemain}"
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

let lines = File.ReadAllLines("fsharp/day10/simpletest.txt") |> Array.toList
let buffer = lines |> List.map parseInstruction
let history = processRegister buffer

printfn "%A" history
// getStateAtCycle history 0
getStateAtCycle history 1
getStateAtCycle history 2
getStateAtCycle history 3
getStateAtCycle history 4
getStateAtCycle history 5
// getStateAtCycle history 6
// getStateAtCycle history 7
// printfn "Value of X at cycle %i was %i" 18 (getValueAtCycle history 18)
// printfn "Value of X at cycle %i was %i" 19 (getValueAtCycle history 19)
// printfn "Value of X at cycle %i was %i" 20 (getValueAtCycle history 20)
// printfn "Value of X at cycle %i was %i" 21 (getValueAtCycle history 21)
// printfn "Value of X at cycle %i was %i" 22 (getValueAtCycle history 22)
// printfn "Value of X at cycle %i was %i" 60 (getValueAtCycle history 60)
// printfn "Value of X at cycle %i was %i" 100 (getValueAtCycle history 100)
// printfn "Value of X at cycle %i was %i" 140 (getValueAtCycle history 140)
// printfn "Value of X at cycle %i was %i" 180 (getValueAtCycle history 180)
// printfn "Value of X at cycle %i was %i" 220 (getValueAtCycle history 220)