namespace AdventOfCode2022.Core.Day11.Exceptions;

public class NoItemsException : Exception
{
  public NoItemsException(string message) : base(message)
  {
  }
}