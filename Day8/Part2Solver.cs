using Common;

namespace Day8;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return Enumerable.Range(1, lines.Length - 1)
            .SelectMany(i => Enumerable.Range(1, lines[0].Length - 1).Select(j => (i, j)))
            .Max(coordinates => Enum.GetValues<TreeMatrixHelpers.Directions>()
                .Select(direction => TreeMatrixHelpers.CountScenicScore(lines, coordinates.i, coordinates.j, direction))
                .Aggregate(1, (acc, x) => acc * x));
    }
}