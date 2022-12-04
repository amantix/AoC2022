using Common;

namespace Day4;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return lines
            .Select(line
                => line
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(range => range.Split('-').Select(int.Parse).ToArray())
                    .ToArray())
            .Count(pair
                => pair[0][0] >= pair[1][0] && pair[0][1] <= pair[1][1]
                   || pair[1][0] >= pair[0][0] && pair[1][1] <= pair[0][1]);
    }
}