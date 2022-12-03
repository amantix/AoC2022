using Common;

namespace Day3;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return Enumerable.Range(0, lines.Length / 3)
            .SelectMany(i =>
                lines[3 * i].Intersect(lines[3 * i + 1]).Intersect(lines[3 * i + 2])
                    .Select(letter => char.IsLower(letter) ? letter - 'a' + 1 : letter - 'A' + 27))
            .Sum();
    }
}