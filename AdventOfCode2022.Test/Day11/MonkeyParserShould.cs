using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;
using NUnit.Framework.Constraints;

namespace AdventOfCode2022.Test.Day11;

[TestFixture]
public class MonkeyParserShould
{
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
    var monkey0 = new Monkey(new List<int> { 79, 98 }, 19, new MultiplyStrategy());
    var monkey1 = new Monkey(new List<int> { 54, 65, 75, 74 }, 6, new AddStrategy());
    var monkey2 = new Monkey(new List<int> { 79, 60, 97 }, 19, new MultiplyStrategy());
    var monkey3 = new Monkey(new List<int> { 74 }, 19, new AddStrategy());
  }

  [Test]
  public void Return_Monkeys()
  {
    var expectedMonkeys = new List<Monkey>();
    var parser = new MonkeyParser();
    Assert.That(parser.Monkeys(), Is.EqualTo(expectedMonkeys));
  }
}