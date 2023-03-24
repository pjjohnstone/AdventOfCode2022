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

let signalStrength history cycle =
  printfn "Signal strength at cycle %i was %i" cycle (getValueAtCycle history cycle * cycle)

let sumSignalStrengths history cycles =
  let sum =
    cycles
    |> List.map (fun c -> getValueAtCycle history c * c)
    |> List.sum
  printf "Sum signal strength is %i" sum

let lines = File.ReadAllLines("fsharp/day10/input.txt") |> Array.toList
let buffer = (lines |> List.map parseInstruction)@[{Value = 0; CyclesRemain = 1; Label = "terminator"}]
let history = processRegister buffer
let importantCycles = [20;60;100;140;180;220]

importantCycles |> List.iter (fun c -> signalStrength history c)
sumSignalStrengths history importantCycles