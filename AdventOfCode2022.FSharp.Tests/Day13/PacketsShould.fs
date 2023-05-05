namespace AdventOfCode2022.FSharp.Tests.Day13

open NUnit.Framework

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
    Packets.inOrder s
