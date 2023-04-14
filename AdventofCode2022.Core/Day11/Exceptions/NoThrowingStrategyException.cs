namespace AdventOfCode2022.Core.Day11.Exceptions;

public class NoThrowingStrategyException : Exception
{
  public NoThrowingStrategyException(string message) : base(message)
  {
  }
}