open System.IO

type Item = {
  Name: string
  Type: string
  Size: int64
  Parent: string
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
      Size = (if size = "dir" then 0 else (size |> int64));
      Parent = wd
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
  let root = { Name = "/"; Type = "Directory"; Size = 0; Parent = "-"}
  buildStructureRec [root] "/" lines

let getDirContents dir structure =
  structure
  |> List.filter (fun i -> i.Parent = dir)

let getDirTotalSize structure dir =
  let rec getDirTotalSizeRec size (contents: Item list) structure =
    match contents with
    | [] -> size
    | _ ->
      match contents.Head.Type with
      | "Directory" ->
        getDirTotalSizeRec (getDirTotalSizeRec size (getDirContents contents.Head.Name structure) structure) contents.Tail structure
      | _ ->
        getDirTotalSizeRec (contents.Head.Size + size) contents.Tail structure
  getDirTotalSizeRec 0 (getDirContents dir structure) structure

let lines = File.ReadAllLines "fsharp/day7/input.txt" |> Array.toList

let structure = buildStructure lines

let dirs =
  structure
  |> List.filter (fun i -> i.Type = "Directory")
  |> List.map (fun i -> i.Name)

let files =
  structure
  |> List.filter (fun i -> i.Type = "File")
  |> List.map (fun i -> i.Name)

let firstSize = getDirTotalSize structure (List.last dirs)

// let dirSizes =
//   dirs
//   |> List.map (getDirTotalSize structure)

// let maxUnder100k =
//   dirSizes
//   |> List.filter (fun s -> s < 100000)
//   |> List.sum
