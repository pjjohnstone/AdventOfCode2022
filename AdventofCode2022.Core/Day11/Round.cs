namespace AdventOfCode2022.Core.Day11;

public class Round
{
  private readonly IMonkeyParser _parser;
  private readonly IEnumerable<string> _inputData;

  public Round(IMonkeyParser parser, IEnumerable<string> inputData)
  {
    _parser = parser;
    _inputData = inputData;
    Monkeys = new List<Monkey>();
  }

  public List<Monkey> Monkeys { get; private set; }

  public void Initialise()
  {
    Monkeys = _parser.Monkeys(_inputData).ToList();
  }
}