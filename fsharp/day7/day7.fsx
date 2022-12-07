open System.IO

type Item = {
  Name: string
  Type: string
  Size: int
  Parent: string Option
}

let stripWhiteSpace ((a: char[]),(b: char[])) =
  (a |> Array.filter (fun c -> c <> ' ') |> System.String),
  (b |> Array.filter (fun c -> c <> ' ') |> System.String)

let makeItem wd line =
  line
  |> Seq.toArray
  |> fun a -> Array.splitAt (Array.findIndex (fun c -> c = ' ') a) a
  |> stripWhiteSpace
  |> fun (size, name) ->
    {
      Name = name;
      Type = (if size = "dir" then "Directory" else "File");
      Size = (if size = "dir" then 0 else (size |> int));
      Parent = Some(wd)
    }

let parseCommand (wd: string) (command: string) =
  if command[2] = 'c' then
    command |> Seq.toArray |> Array.removeManyAt 0 5 |> System.String
  else
    wd

let buildStructure (lines: string list) =
  let rec buildStructureRec structure wd lines =
    match lines with
    | [] -> structure
    | _ ->
      match (lines.Head |> Seq.toArray |> Array.head) with
      | 'd' -> buildStructureRec ((makeItem wd lines.Head)::structure) wd lines.Tail
      | '$' -> buildStructureRec structure (parseCommand wd lines.Head) lines.Tail
      | _ -> buildStructureRec ((makeItem wd lines.Head)::structure) wd lines.Tail
  buildStructureRec [] "/" lines

let lines = File.ReadAllLines "fsharp/day7/test.txt" |> Array.toList

let structure = buildStructure lines
