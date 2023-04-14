namespace AdventofCode2022.Core.Day11;

public class Monkey
{
  private readonly int _operation;
  public List<int> Items { get; }

  public Monkey(List<int> items, int operation)
  {
    _operation = operation;
    Items = items;
    throw new NotImplementedException();
  }
}