namespace AdventOfCode2022.Core.Day11;

public interface IMonkeyParser
{
  IEnumerable<Monkey> Monkeys(IEnumerable<string> input);
}