using Common;

namespace Day4;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return lines
            .Select(line => line
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(range => range.Split('-').Select(int.Parse).ToArray())
                .SelectMany(x => Enumerable.Range(x[0], x[1] - x[0] + 1))
                .ToArray())
            .Count(pair => pair.Distinct().Count() < pair.Length);
    }
}