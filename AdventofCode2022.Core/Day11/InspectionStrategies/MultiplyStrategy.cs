namespace AdventOfCode2022.Core.Day11.InspectionStrategies;

public class MultiplyStrategy : InspectionStrategy
{
  public override int Inspect(int worry, int operationValue) => worry * operationValue;
}