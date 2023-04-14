using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Core.Day11;

public static class MonkeyParser
{
  public static List<Monkey> Monkeys(IEnumerable<string> input)
  {
    var monkeys = new List<Monkey>();
    var blocks = input.Chunk(6);
    foreach (var block in blocks)
    {
      var monkeyNumber = int.Parse(block.First().Replace(":", "").Split(' ')[1]);
      var startingItems = block[1].Replace(" ", "").Split(':')[1].Split(',').Select(int.Parse).ToList();
      var operand = block[2].Split(' ')[^2];
      var value = block[2].Split(' ')[^1];
      if (operand == "*")
      {
        monkeys.Add(int.TryParse(value, out var valueInt)
          ? new Monkey(monkeyNumber, startingItems, new MultiplyStrategy(valueInt))
          : new Monkey(monkeyNumber, startingItems, new SelfMultiplyStrategy()));
      }
      else
      {
        monkeys.Add(int.TryParse(value, out var valueInt)
          ? new Monkey(monkeyNumber, startingItems, new AddStrategy(valueInt))
          : new Monkey(monkeyNumber, startingItems, new SelfAddStrategy()));
      }
    }

    return monkeys;
  }
}