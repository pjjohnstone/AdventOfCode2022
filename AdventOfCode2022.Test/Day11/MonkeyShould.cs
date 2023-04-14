using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Test.Day11
{
  public class MonkeyShould
  {
    [TestCaseSource(nameof(_inspectCases))]
    public void Change_Worry_Level_On_Inspection(List<int> startingValues, int operationValue,
      InspectionStrategy strategy,
      List<int> resultingWorryValues)
    {
      var monkey = new Monkey(startingValues, operationValue, strategy);

      monkey.Inspect();

      Assert.That(monkey.Items, Is.EqualTo(resultingWorryValues));
    }

    [Test]
    public void Catch_An_Item()
    {
      var expectedItems = new List<int> {1, 2, 3};
      var monkey = new Monkey(new List<int>{1,2}, 10, new MultiplyStrategy());

      monkey.Catch(3);

      Assert.That(monkey.Items, Is.EqualTo(expectedItems));
    }

    private static object[] _inspectCases =
    {
      new object[] { new List<int> { 1, 2 }, 10, new MultiplyStrategy(), new List<int> { 3, 2 } },
      new object[] { new List<int> { 1, 2 }, 10, new AddStrategy(), new List<int> { 3, 2 } }
    };
  }
}