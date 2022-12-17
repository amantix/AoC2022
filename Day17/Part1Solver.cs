using Common;

namespace Day17;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return Chamber.Simulate(lines[0], 2022).Height;
    }
}