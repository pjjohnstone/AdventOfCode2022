using AdventOfCode2022.Core.Day11.InspectionStrategies;

namespace AdventOfCode2022.Core.Day11;

public static class MonkeyParser
{
  public static IEnumerable<Monkey> Monkeys(IEnumerable<string> input)
  {
    var monkeys = new List<Monkey>();
    var throwingStrategyDefinitions = new List<ThrowingStrategyDefinition>();
    var blocks = input.Chunk(6);
    foreach (var block in blocks)
    {
      var monkeyNumber = int.Parse(block.First().Replace(":", "").Split(' ')[1]);
      var startingItems = block[1].Replace(" ", "").Split(':')[1].Split(',').Select(int.Parse).ToList();
      var operand = block[2].Split(' ')[^2];
      var operationValue = block[2].Split(' ')[^1];
      
      if (operand == "*")
      {
        monkeys.Add(int.TryParse(operationValue, out var valueInt)
          ? new Monkey(monkeyNumber, startingItems, new MultiplyStrategy(valueInt))
          : new Monkey(monkeyNumber, startingItems, new SelfMultiplyStrategy()));
      }
      else
      {
        monkeys.Add(int.TryParse(operationValue, out var valueInt)
          ? new Monkey(monkeyNumber, startingItems, new AddStrategy(valueInt))
          : new Monkey(monkeyNumber, startingItems, new SelfAddStrategy()));
      }

      throwingStrategyDefinitions.Add(DefineStrategy(block, monkeyNumber));
    }

    foreach (var monkey in monkeys)
    {
      monkey.ThrowingStrategy = AssignStrategy(throwingStrategyDefinitions, monkey, monkeys);
    }

    return monkeys;
  }

  private static ThrowingStrategy AssignStrategy(IEnumerable<ThrowingStrategyDefinition> throwingStrategyDefinitions, Monkey monkey, IReadOnlyCollection<Monkey> monkeys)
  {
    var definition = throwingStrategyDefinitions.First(d => d.MonkeyNumber.Equals(monkey.Number));
    var trueMonkey = monkeys.First(m => m.Number.Equals(definition.TrueMonkeyNumber));
    var falseMonkey = monkeys.First(m => m.Number.Equals(definition.FalseMonkeyNumber));
    return new ThrowingStrategy(definition.TestValue, trueMonkey, falseMonkey);
  }

  private static ThrowingStrategyDefinition DefineStrategy(IReadOnlyList<string> block, int monkeyNumber)
  {
    var testValue = int.Parse(block[3].Split(' ')[^1]);
    var trueMonkeyNumber = int.Parse(block[4].Split(' ')[^1]);
    var falseMonkeyNumber = int.Parse(block[5].Split(' ')[^1]);
    return new ThrowingStrategyDefinition
    {
      MonkeyNumber = monkeyNumber,
      TestValue = testValue,
      TrueMonkeyNumber = trueMonkeyNumber,
      FalseMonkeyNumber = falseMonkeyNumber
    };
  }
}