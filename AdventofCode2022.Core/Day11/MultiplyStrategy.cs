using AdventOfCode2022.Core.Day11;

namespace AdventofCode2022.Core.Day11;

public class MultiplyStrategy : MonkeyStrategy
{
  public override int Inspect(int worry, int operationValue) => worry * operationValue;
}