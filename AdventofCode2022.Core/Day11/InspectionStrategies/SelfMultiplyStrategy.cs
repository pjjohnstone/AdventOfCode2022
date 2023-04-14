namespace AdventOfCode2022.Core.Day11.InspectionStrategies;

public class SelfMultiplyStrategy : InspectionStrategy
{
  public override int Inspect(int worry) => worry * worry;
}