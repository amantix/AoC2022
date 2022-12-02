using Common;

namespace Day2;

using static RockPaperScissors;

public class Solver<T> : ISolver<int> where T : struct
{
    public int Solve(string[] lines)
    {
        return lines
            .Select(ParseRound<T>)
            .Sum(round => round.Score);
    }
}