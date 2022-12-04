open System.IO

let lines = File.ReadAllLines("fsharp/day4/test.txt") |> Array.toList

let splitLine line =
  line
  |> Seq.findIndex (fun c -> c = ',')
  |> fun i -> line |> Seq.toList |> List.splitAt i

let removeSymbols charList =
  charList
  |> List.filter (fun c -> System.Char.IsNumber(c))

let removeSymbolsFromPair (first,second) =
  (removeSymbols first, removeSymbols second)

let getPairs lines =
  lines
  |> List.map splitLine
  |> List.map removeSymbolsFromPair

let pairs = getPairs lines
