using Common;

namespace Day11;

public class Part1Solver : ISolver<long>
{
    public long Solve(string[] lines)
    {
        var monkeys = MonkeyInTheMiddle.InitMonkeys(lines);
        monkeys = monkeys.Select(monkey => monkey with { Operation = value => monkey.Operation(value) / 3 }).ToList();
        var counters = MonkeyInTheMiddle.GetCountersAfterSimulation(monkeys, 20);
        return counters.OrderByDescending(x => x).Take(2).Aggregate(1, (acc, x) => acc * x);
    }
}