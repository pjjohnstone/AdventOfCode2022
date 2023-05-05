// See https://aka.ms/new-console-template for more information

using AdventOfCode2022.Core.Day11;

Console.WriteLine("Day11, Part1");
var inputData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Day11/input.txt")).Where(s => !string.IsNullOrEmpty(s));
var round = new Round(new MonkeyParser(), inputData);
round.Initialise();
for (int i = 0; i < 20; i++)
{
  round.PlayRound();
}
var topMonkeys = round.Monkeys.OrderByDescending(m => m.Inspections).Take(2);
Console.WriteLine($"Monkey business this round is: {topMonkeys.Select(m => m.Inspections).Aggregate((x,y) => x * y)}");

Console.WriteLine("Day13, Part1");
var result = Packets.ProcessSignals(Path.Combine(Environment.CurrentDirectory, "Day13/input.txt"));
Console.WriteLine($"Sum of indices of correct packets is {result}");