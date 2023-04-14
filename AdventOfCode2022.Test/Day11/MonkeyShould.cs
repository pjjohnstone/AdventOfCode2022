using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Test.Day11
{
  public class MonkeyShould
  {
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(InspectCases))]
    public void Change_Worry_Level_On_Inspection(List<int> startingValues, int operationValue, InspectionStrategy strategy,
      int resultingWorry)
    {
      var monkey = new Monkey(startingValues, operationValue, strategy);

      monkey.Inspect();

      Assert.That(monkey.Items.First(), Is.EqualTo(resultingWorry));
    }

    public static object[] InspectCases =
    {
      new object[] { new List<int> { 1 }, 10, new MultiplyStrategy(), 3 },
      new object[] { new List<int> { 1 }, 10, new AddStrategy(), 3 }
    };
  }
}