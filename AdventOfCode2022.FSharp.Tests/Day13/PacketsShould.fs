namespace AdventOfCode2022.FSharp.Tests.Day13

open NUnit.Framework

[<TestFixture>]
module PacketsShould =

  [<SetUp>]
  let Setup () =
      ()

  [<Test>]
  let ``Group numbers from char lists`` () =
    let testData = "1,1,3,1,1" |> Seq.toArray |> Array.toList
    let expectedResult = [1;1;3;1;1]
    let actualResult = Packets.groupNumbers testData
    Assert.That(actualResult, Is.EqualTo expectedResult)

  let charIntConversionCases =
    [
      TestCaseData(['1';'2']).Returns(12);
      TestCaseData(['1']).Returns(1);
      TestCaseData(['1';'3';'2']).Returns(132);
    ]

  [<TestCaseSource("charIntConversionCases")>]
  let ``Convert char list to int`` s =
    Packets.intFromCharList s
