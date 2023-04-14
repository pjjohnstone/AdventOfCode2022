using AdventofCode2022.Core.Day11;

namespace AdventOfCode2022.Test.Day11
{
  public class MonkeyShould
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Change_Worry_Level_On_Inspection()
    {
      var expectedWorryLevel = 10;
      var operation = 10;
      var items = new List<int>{ 1 };
      var monkey = new Monkey(items, operation);
      Assert.That(monkey.Items.First(), Is.EqualTo(expectedWorryLevel));
    }
  }
}