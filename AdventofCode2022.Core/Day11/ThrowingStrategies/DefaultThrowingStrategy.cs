using AdventOfCode2022.Core.Day11.Exceptions;

namespace AdventOfCode2022.Core.Day11.ThrowingStrategies;

public class DefaultThrowingStrategy : IThrowingStrategy
{
  public void Throw(int itemValue)
  {
    throw new NoThrowingStrategyException("There is no throwing strategy set!");
  }
}