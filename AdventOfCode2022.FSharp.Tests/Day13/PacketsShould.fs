namespace AdventOfCode2022.FSharp.Tests.Day13

open NUnit.Framework
open Packets

[<TestFixture>]
module PacketsShould =

  [<SetUp>]
  let Setup () =
      ()

  let inOrderTestCases = 
    [
      TestCaseData("[1,1,3,1,1]", "[1,1,5,1,1]").Returns(true)
      TestCaseData("[[1],[2,3,4]]", "[[1],4]").Returns(true)
      TestCaseData("[9]", "[[8,7,6]]").Returns(false)
      TestCaseData("[[4,4],4,4]", "[[4,4],4,4,4]").Returns(true)
      TestCaseData("[7,7,7,7]", "[7,7,7]").Returns(false)
      TestCaseData("[]", "[3]").Returns(true)
      TestCaseData("[[[]]]", "[[]]").Returns(false)
      TestCaseData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]").Returns(false)
    ]

  [<TestCaseSource("inOrderTestCases")>]
  let ``Compare left and right packets`` s =
    inOrder s

  let parsingCases = 
    [
      TestCaseData(
        [|
          "[1,1,3,1,1]"
          "[1,1,5,1,1]"
          ""
          "[[1],[2,3,4]]"
          "[[1],4]"
          ""
        |]
      ).Returns(
        [
          ("[1,1,3,1,1]", "[1,1,5,1,1]")
          ("[[1],[2,3,4]]", "[[1],4]")
        ]
      )
    ]

  [<TestCaseSource("parsingCases")>]
  let ``Parse input data into tuples of strings`` s =
    pairs s

  let indexSumCases = 
    [
      TestCaseData([true;true;false;true]).Returns(7)
      TestCaseData([false;true;false;true]).Returns(6)
      TestCaseData([false;false;false;true]).Returns(4)
    ]

  [<TestCaseSource("indexSumCases")>]
  let ``Sum indices of correctly ordered packets`` s =
    sumIndices s

  let symbolOrNumberCases =
    [
      TestCaseData("[1,1,3]").Returns([
        Char('[')
        Number(1)
        Char(',')
        Number(1)
        Char(',')
        Number(3)
        Char(']')
      ])
      TestCaseData("[1,2,10]").Returns([
        Char('[')
        Number(1)
        Char(',')
        Number(2)
        Char(',')
        Number(10)
        Char(']')
      ])
    ]

  [<TestCaseSource("symbolOrNumberCases")>]
  let ``Convert char list to list of Symbols`` s =
    charToSymbols s