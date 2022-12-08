using Common;

namespace Day8;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var borders = Enumerable.Range(0, lines.Length)
            .SelectMany(i => new[] { (i, 0), (i, lines[0].Length - 1) })
            .Concat(Enumerable.Range(0, lines[0].Length)
                .SelectMany(j => new[] { (0, j), (lines.Length - 1, j) })
            );

        var leftToRight = Enumerable.Range(1, lines.Length - 2)
            .SelectMany(i => Enumerable.Range(1, lines[0].Length - 2)
                .Select(j => (i, j))
                .FilterByMax(lines, lines[i][0])
            );

        var rightToLeft = Enumerable.Range(1, lines.Length - 2)
            .SelectMany(i => Enumerable.Range(0, lines[0].Length - 1)
                .Reverse()
                .Select(j => (i, j))
                .FilterByMax(lines, lines[i][^1])
            );

        var topDown = Enumerable.Range(1, lines.Length - 2)
            .SelectMany(j => Enumerable.Range(1, lines[0].Length - 2)
                .Select(i => (i, j))
                .FilterByMax(lines, lines[0][j])
            );

        var bottomUp = Enumerable.Range(1, lines.Length - 2)
            .SelectMany(j => Enumerable.Range(0, lines[0].Length - 1)
                .Reverse()
                .Select(i => (i, j))
                .FilterByMax(lines, lines[^1][j])
            );

        return borders
            .Concat(leftToRight)
            .Concat(rightToLeft)
            .Concat(topDown)
            .Concat(bottomUp)
            .Distinct()
            .Count();
    }
}