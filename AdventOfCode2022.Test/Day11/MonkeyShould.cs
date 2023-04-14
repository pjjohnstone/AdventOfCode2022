using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.Exceptions;
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

    [TestCase(5,true)]
    [TestCase(6,false)]
    public void Test_And_Throw_Item(int value, bool result)
    {
      var monkey = new Monkey(new List<int>{value}, 3, new MultiplyStrategy());
      var monkey2 = new Monkey(new List<int>(), 5, new AddStrategy());
      var monkey3 = new Monkey(new List<int>(), 5, new AddStrategy());
      var strategy = new ThrowingStrategy(5, monkey2, monkey3);
      monkey.ThrowingStrategy = strategy;

      monkey.Throw();

      Assert.That(monkey2.Items.Contains(value), Is.EqualTo(result));
      Assert.That(monkey3.Items.Contains(value), Is.Not.EqualTo(result));
    }

    [Test]
    public void Throw_Exception_If_Throw_And_No_Strategy()
    {
      var monkey = new Monkey(new List<int>(), 1, new MultiplyStrategy());
      Assert.Throws<NoThrowingStrategyException>(() => monkey.Throw());
    }

    private static object[] _inspectCases =
    {
      new object[] { new List<int> { 1, 2 }, 10, new MultiplyStrategy(), new List<int> { 3, 2 } },
      new object[] { new List<int> { 1, 2 }, 10, new AddStrategy(), new List<int> { 3, 2 } }
    };
  }
}