using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Core.Day11;

public class Monkey
{
  private readonly int _operationValue;
  private readonly InspectionStrategy _strategy;
  public List<int> Items { get; }

  public Monkey(List<int> items, int operationValue, InspectionStrategy strategy)
  {
    _operationValue = operationValue;
    _strategy = strategy;
    Items = items;
  }

  public void Inspect()
  {
    var newItemWorry = _strategy.Inspect(Items.First(), _operationValue) / 3;
    Items.RemoveAt(0);
    Items.Insert(0, newItemWorry);
  }

  public void Catch(int itemValue)
  {
    Items.Add(itemValue);
  }
}