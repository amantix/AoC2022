using Common;

namespace Day19;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var bluePrints = BluePrint.ParseBluePrints(lines);
        var timeLimit = 32;
        var result = bluePrints
            .Take(3)
            .Select(bluePrint => bluePrint.GetMaxGeodeCount(bluePrint, timeLimit))
            .ToArray();
        return result
            .Aggregate(1, (acc, current) => acc * current);
    }
}