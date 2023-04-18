using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;
using Moq;

namespace AdventOfCode2022.Test.Day11;

[TestFixture]
public class RoundShould
{
  [Test]
  public void Call_MonkeyParser_To_Initialise_Monkeys()
  {
    var monkey0 = new Monkey(0, new List<int>(), new MultiplyStrategy(3));
    var parser = new Mock<IMonkeyParser>();
    var inputData = new List<string> { "test" };
    parser.Setup(p => p.Monkeys(It.Is<IEnumerable<string>>(d => d.Equals(inputData)))).Returns(new List<Monkey>{monkey0});
    var round = new Round(parser.Object, inputData);

    round.Initialise();

    Assert.That(round.Monkeys, Does.Contain(monkey0));
  }
}