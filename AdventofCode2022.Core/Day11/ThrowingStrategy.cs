namespace AdventOfCode2022.Core.Day11;

public class ThrowingStrategy
{
  private readonly Monkey _falseMonkey;

  private readonly int _testValue;
  private readonly Monkey _trueMonkey;

  public ThrowingStrategy(int testValue, Monkey trueMonkey, Monkey falseMonkey)
  {
    _testValue = testValue;
    _trueMonkey = trueMonkey;
    _falseMonkey = falseMonkey;
  }

  public void Throw(int itemValue)
  {
    if (itemValue % _testValue == 0)
      _trueMonkey.Catch(itemValue);
    else
      _falseMonkey.Catch(itemValue);
  }
}