using Common;

namespace Day9;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var movements = lines.ParseMovements();
        var tailPositions = RopeMovementTools.GetTailPath(movements, 2);
        return tailPositions.Distinct().Count();
    }
}