using Microsoft.FSharp.Collections;

namespace AdventOfCode2022.Test.Day13;

[TestFixture]
public class PacketsShould
{
  private string[] _input = null!;

  private const string Sample = @"
[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

  [SetUp]
  public void SetUp()
  {
    _input = Sample.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
  }

  [Test, Ignore("WIP")]
  public void Determine_Packet_Order()
  {
    var expectedResult = new List<bool>{true,true,false,true,false,true,false,false};
    
    Assert.That(Packets.IsInOrder(_input), Is.EqualTo(expectedResult));
  }

  [Test]
  public void Returns_Packet_Pairs()
  {
    var expectedPairs = new List<List<string>>
    {
      new() {"[1,1,3,1,1]", "[1,1,5,1,1]"},
      new() { "[[1],[2,3,4]]", "[[1],4]" },
      new() { "[9]", "[[8,7,6]]" },
      new() { "[[4,4],4,4]", "[[4,4],4,4,4]"},
      new() { "[7,7,7,7]", "[7,7,7]"},
      new() { "[]", "[3]"},
      new() { "[[[]]]", "[[]]"},
      new() { "[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]"}
    };

    Assert.That(Packets.pairs(_input), Is.EqualTo(expectedPairs));
  }
}