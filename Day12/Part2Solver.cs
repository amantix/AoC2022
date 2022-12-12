using Common;

namespace Day12;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var map = lines.Select(line => line.ToArray()).ToArray();

        var (startRow, startColumn) = map.SelectPositions(c => c == 'S').Single();
        var (endRow, endColumn) = map.SelectPositions(c => c == 'E').Single();
        map[startRow][startColumn] = 'a';
        map[endRow][endColumn] = 'z';

        var initialPositions = map.SelectPositions(c => c == 'a').ToArray();
        var targetPositions = new[] { (endRow, endColumn) };
        return map.CountPathLength(initialPositions, targetPositions, CheckStep);

        bool CheckStep((int row, int column) from, (int row, int column) to)
            => map[to.row][to.column] - map[from.row][from.column] <= 1;
    }
}