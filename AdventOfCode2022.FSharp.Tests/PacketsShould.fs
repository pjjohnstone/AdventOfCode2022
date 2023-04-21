module AdventOfCode2022.FSharp.Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Group_Numbers () =
  let testData = "1,1,3,1,1" |> Seq.toArray |> Array.toList
  let expectedResult = [1;1;3;1;1]
  let actualResult = Packets.groupNumbers testData
  Assert.That(actualResult, Is.EqualTo expectedResult)

[<Test>]
let Converts_Char_List_To_Int () =
  Assert.That(Packets.intFromCharList ['1';'2'], Is.EqualTo 12)
