using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Test.Day11;

[TestFixture]
public class RoundShould
{
  [Test,Ignore("WIP")]
  public void Call_MonkeyParser_To_Initialise_Monkeys()
  {
    var monkey0 = new Monkey(0, new List<int>(), new MultiplyStrategy(3));
    var round = new Round();
    Assert.That(round.Monkeys.Contains(monkey0));
  }
}