using AdventOfCode2022.Core.Day11;
using AdventOfCode2022.Core.Day11.Exceptions;
using AdventOfCode2022.Core.Day11.InspectionStrategies;
using Newtonsoft.Json.Linq;

namespace AdventOfCode2022.Test.Day11
{
  public class MonkeyShould
  {
    private Monkey _monkey = null!;
    private Monkey _monkey2 = null!;
    private Monkey _monkey3 = null!;

    [SetUp]
    public void SetUp()
    {
      _monkey = new Monkey(new List<int>(), new MultiplyStrategy(3));
      _monkey2 = new Monkey(new List<int>(), new AddStrategy(5));
      _monkey3 = new Monkey(new List<int>(), new AddStrategy(5));
    }

    [TestCaseSource(nameof(_inspectCases))]
    public void Change_Worry_Level_On_Inspection(
      List<int> startingValues,
      InspectionStrategy strategy,
      List<int> resultingWorryValues
    )
    {
      var monkey = new Monkey(startingValues, strategy);

      monkey.Inspect();

      Assert.That(monkey.Items, Is.EqualTo(resultingWorryValues));
    }

    [Test]
    public void Catch_An_Item()
    {
      var expectedItems = new List<int> { 1, 2, 3 };
      var monkey = new Monkey(new List<int> { 1, 2 }, new MultiplyStrategy(10));

      monkey.Catch(3);

      Assert.That(monkey.Items, Is.EqualTo(expectedItems));
    }

    [TestCase(5, true)]
    [TestCase(6, false)]
    public void Test_And_Throw_Item(int value, bool result)
    {
      _monkey.Catch(value);
      var strategy = new ThrowingStrategy(5, _monkey2, _monkey3);
      _monkey.ThrowingStrategy = strategy;

      _monkey.Throw();

      Assert.Multiple(() =>
      {
        Assert.That(_monkey2.Items.Contains(value), Is.EqualTo(result));
        Assert.That(_monkey3.Items.Contains(value), Is.Not.EqualTo(result));
      });
    }

    [Test]
    public void Throw_Exception_If_Throw_And_No_Strategy()
    {
      Assert.Throws<NoThrowingStrategyException>(() => _monkey.Throw());
    }

    [Test]
    public void Not_Throw_Items_More_Than_Once()
    {
      _monkey.ThrowingStrategy = new ThrowingStrategy(1, _monkey2, _monkey3);
      _monkey.Catch(1);
      _monkey.Catch(2);

      _monkey.Throw();
      _monkey.Throw();

      Assert.Multiple(() =>
      {
        Assert.That(_monkey2.Items.Distinct().Count(), Is.EqualTo(2));
        Assert.That(_monkey.Items.Count, Is.EqualTo(0));
      });
    }

    private static object[] _inspectCases =
    {
      new object[] { new List<int> { 1, 2 }, new MultiplyStrategy(10), new List<int> { 3, 2 } },
      new object[] { new List<int> { 1, 2 }, new AddStrategy(10), new List<int> { 3, 2 } },
      new object[] { new List<int> { 3, 2 }, new SelfMultiplyStrategy(), new List<int> { 3, 2 } },
      new object[] { new List<int> { 3, 2 }, new SelfAddStrategy(), new List<int> { 2, 2 } }
    };
  }
}