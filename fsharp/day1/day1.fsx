open System.IO

let lines = File.ReadAllLines("fsharp/day1/test.txt") |> Array.toList

let indexOfSpace (lines: string list) =
  lines |> List.findIndex (fun l -> String.length l < 1)

type Elf = { Items: int list }

let parseCalories c =
  System.Int32.Parse c

let spawnElf (inventory: string list) =
  inventory
  |> List.filter (fun l -> l.Length > 1)
  |> List.map (fun l -> parseCalories l)
  |> fun i -> { Items = i }

let getElves lines =
  let rec getElvesRec lines elves =
    match lines with
    | [] -> elves
    | _ ->
      match (List.exists (fun l -> String.length l < 1) lines) with
      | true ->
        lines
        |> List.splitAt (indexOfSpace lines)
        |> fun (h,t) -> getElvesRec t.Tail ((spawnElf h)::elves)
      | false ->
        getElvesRec [] ((spawnElf lines)::elves)
  getElvesRec lines []

printfn "%A" (getElves lines)