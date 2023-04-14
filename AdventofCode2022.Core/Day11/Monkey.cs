using AdventOfCode2022.Core.Day11.Exceptions;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Core.Day11;

public class Monkey
{
  private readonly int _operationValue;
  private readonly InspectionStrategy _inspectionStrategy;
  public ThrowingStrategy? ThrowingStrategy { get; set; }
  public List<int> Items { get; }

  public Monkey(List<int> items, InspectionStrategy inspectionStrategy)
  {
    _inspectionStrategy = inspectionStrategy;
    Items = items;
  }

  public void Inspect()
  {
    var newItemWorry = _inspectionStrategy.Inspect(Items.First()) / 3;
    Items.RemoveAt(0);
    Items.Insert(0, newItemWorry);
  }

  public void Catch(int itemValue)
  {
    Items.Add(itemValue);
  }

  public void Throw()
  {
    if (ThrowingStrategy == null) throw new NoThrowingStrategyException("There is no throwing strategy set!");
    ThrowingStrategy.Throw(Items.First());
    Items.RemoveAt(0);
  }
}