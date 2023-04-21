namespace AdventOfCode2022.FSharp.Tests.Day13

open NUnit.Framework

[<TestFixture>]
module PacketsShould =

  [<SetUp>]
  let Setup () =
      ()

  let groupNumbersCases =
    [
      TestCaseData("1,1,3,1,1" |> Seq.toArray |> Array.toList).Returns([1;1;3;1;1])
      TestCaseData("2,3,4" |> Seq.toArray |> Array.toList).Returns([2;3;4])
    ]

  [<TestCaseSource("groupNumbersCases")>]
  let ``Group numbers from char lists`` s =
    Packets.groupNumbers s

  let charIntConversionCases =
    [
      TestCaseData(['1';'2']).Returns(12)
      TestCaseData(['1']).Returns(1)
      TestCaseData(['2']).Returns(2)
      TestCaseData(['1';'3';'2']).Returns(132)
    ]

  [<TestCaseSource("charIntConversionCases")>]
  let ``Convert char list to int`` s =
    Packets.intFromCharList s

  let groupsCases =
    [
      TestCaseData("[1,1,3,1,1]").Returns("1,1,3,1,1" |> Seq.toArray |> Array.toList)
      TestCaseData("[[1],[2,3,4]]").Returns("[1],[2,3,4]" |> Seq.toArray |> Array.toList)
    ]

  [<TestCaseSource("groupsCases")>]
  let ``Convert packet into groups`` s =
    Packets.groups s

  let packetCases =
    [
      TestCaseData("[1],[2,3,4]" |> Seq.toArray |> Array.toList).Returns(
        { 
          Packets.Groups = 
          [
            {Values = [1]; Enclosed = true}
            {Values = [2;3;4]; Enclosed = true}
          ]
        }
      )
      TestCaseData("1,1,3,1,1" |> Seq.toArray |> Array.toList).Returns(
        {
          Packets.Groups =
          [
            {Values = [1]; Enclosed = false}
            {Values = [1]; Enclosed = false}
            {Values = [3]; Enclosed = false}
            {Values = [1]; Enclosed = false}
            {Values = [1]; Enclosed = false}
          ]
        }
      )
    ]

  [<TestCaseSource("packetCases")>]
  let ``Convert packet string into types`` s =
    Packets.packets s
