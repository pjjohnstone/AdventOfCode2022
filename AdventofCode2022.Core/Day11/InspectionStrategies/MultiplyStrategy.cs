namespace AdventOfCode2022.Core.Day11.InspectionStrategies;

public class MultiplyStrategy : InspectionStrategy
{
  private readonly int _operationValue;

  public MultiplyStrategy(int operationValue)
  {
    _operationValue = operationValue;
  }

  public override int Inspect(int worry) => worry * _operationValue;
}