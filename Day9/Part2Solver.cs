using Common;

namespace Day9;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var movements = lines.ParseMovements();
        var visited = RopeMovementTools.GetTailPath(movements, 10);
        return visited.Distinct().Count();
    }
}