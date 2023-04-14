namespace AdventOfCode2022.Core.Day11.InspectionStrategies;

public class SelfAddStrategy : InspectionStrategy
{
  public override int Inspect(int worry) => worry + worry;
}