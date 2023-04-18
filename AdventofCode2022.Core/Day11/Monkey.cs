using System.Text.Json;
using AdventOfCode2022.Core.Day11.Exceptions;
using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Core.Day11;

public class Monkey
{
  private readonly InspectionStrategy _inspectionStrategy;

  public Monkey(int number, List<int> items, InspectionStrategy inspectionStrategy)
  {
    _inspectionStrategy = inspectionStrategy;
    Items = items;
    Number = number;
    ThrowingStrategy = new DefaultThrowingStrategy();
  }

  public IThrowingStrategy ThrowingStrategy { get; set; }
  public List<int> Items { get; }
  public int Number { get; }

  private bool Equals(Monkey other)
  {
    var thisJson = JsonSerializer.Serialize(this);
    var otherJson = JsonSerializer.Serialize(other);
    return thisJson.Equals(otherJson);
  }

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    return obj.GetType() == GetType() && Equals((Monkey)obj);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(_inspectionStrategy, Items, Number);
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
    if (Items.Count == 0) throw new NoItemsException($"Called throw on monkey {Number} but there were no items!");
    ThrowingStrategy.Throw(Items.First());
    Items.RemoveAt(0);
  }

  public void TakeTurn()
  {
    while (Items.Count > 0)
    {
      Inspect();
      Throw();
    }
  }
}