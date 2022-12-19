using Common;

namespace Day19;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var bluePrints = BluePrint.ParseBluePrints(lines);
        var timeLimit = 24;
        return bluePrints.Select(bluePrint => bluePrint.Id * bluePrint.GetMaxGeodeCount(bluePrint, timeLimit)).Sum();
    }
}