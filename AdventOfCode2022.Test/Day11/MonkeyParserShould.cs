using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Test.Day11;

[TestFixture]
public class MonkeyParserShould
{
  private Monkey _monkey0 = null!;
  private Monkey _monkey1 = null!;
  private Monkey _monkey2 = null!;
  private Monkey _monkey3 = null!;
  private string[] _inputArray = null!;

  private const string SampleText = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

  [SetUp]
  public void Setup()
  {
    _monkey0 = new Monkey(0, new List<int> { 79, 98 }, new MultiplyStrategy(19));
    _monkey1 = new Monkey(1, new List<int> { 54, 65, 75, 74 }, new AddStrategy(6));
    _monkey2 = new Monkey(2, new List<int> { 79, 60, 97 }, new SelfMultiplyStrategy());
    _monkey3 = new Monkey(3, new List<int> { 74 }, new AddStrategy(3));
    _inputArray = SampleText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
  }

  [Test]
  public void Return_Monkeys()
  {
    var expectedMonkeys = new List<Monkey>
    {
      _monkey0,
      _monkey1,
      _monkey2,
      _monkey3
    };

    Assert.That(MonkeyParser.Monkeys(_inputArray), Is.EqualTo(expectedMonkeys));
  }
}