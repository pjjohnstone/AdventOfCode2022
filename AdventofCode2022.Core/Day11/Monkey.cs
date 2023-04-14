namespace AdventOfCode2022.Core.Day11;

public class Monkey
{
  private readonly int _operationValue;
  private readonly MonkeyStrategy _strategy;
  public List<int> Items { get; }

  public Monkey(List<int> items, int operationValue, MonkeyStrategy strategy)
  {
    _operationValue = operationValue;
    _strategy = strategy;
    Items = items;
  }

  public void Inspect()
  {
    var newItemWorry = _strategy.Inspect(Items.First(), _operationValue);
    Items.RemoveAt(0);
    Items.Insert(0, newItemWorry);
  }
}