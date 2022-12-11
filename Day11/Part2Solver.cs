using Common;

namespace Day11;

public class Part2Solver : ISolver<long>
{
    public long Solve(string[] lines)
    {
        var monkeys = MonkeyInTheMiddle.InitMonkeys(lines);
        var counters = MonkeyInTheMiddle.GetCountersAfterSimulation(monkeys, 10_000);
        return counters.OrderByDescending(x => x).Take(2).Aggregate(1L, (acc, x) => acc * x);
    }
}