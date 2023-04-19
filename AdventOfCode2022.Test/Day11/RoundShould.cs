using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;
using AdventOfCode2022.Core.Day11.ThrowingStrategies;
using Moq;

namespace AdventOfCode2022.Test.Day11;

[TestFixture]
public class RoundShould
{
  private Monkey _monkey0 = null!;
  private Monkey _monkey1 = null!;
  private Monkey _monkey2 = null!;
  private Round _round = null!;

  [SetUp]
  public void SetUp()
  {
    _monkey0 = new Monkey(0, new List<int>{1,2}, new MultiplyStrategy(3));
    _monkey1 = new Monkey(1, new List<int>{3,4}, new MultiplyStrategy(3));
    _monkey2 = new Monkey(2, new List<int>(), new MultiplyStrategy(3));
    var throwingStrategy = new ThrowingStrategy(1, _monkey2, _monkey2);
    _monkey0.ThrowingStrategy = throwingStrategy;
    _monkey1.ThrowingStrategy = throwingStrategy;
    _monkey2.ThrowingStrategy = new ThrowingStrategy(2, _monkey0, _monkey1);
    var parser = new Mock<IMonkeyParser>();
    var inputData = new List<string> { "test" };
    parser.Setup(p => p.Monkeys(It.Is<IEnumerable<string>>(d => d.Equals(inputData)))).Returns(new List<Monkey> { _monkey0, _monkey1, _monkey2 });
    _round = new Round(parser.Object, inputData);
  }

  [Test]
  public void Call_MonkeyParser_To_Initialise_Monkeys()
  {
    _round.Initialise();

    Assert.That(_round.Monkeys, Does.Contain(_monkey0));
  }

  [Test]
  public void Call_All_Monkeys_TakeTurn()
  {
    _round.Initialise();

    _round.PlayRound();

    Assert.Multiple(() =>
    {
      Assert.That(_monkey0.Items, Is.EqualTo(new List<int> { 2, 4 }));
      Assert.That(_monkey1.Items, Is.EqualTo(new List<int> { 1, 3 }));
      Assert.That(_monkey2.Items, Is.Empty);
    });
  }
}