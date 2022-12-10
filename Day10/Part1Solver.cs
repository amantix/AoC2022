using Common;

namespace Day10;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return ProgramTools
            .TraceProgram(lines)
            .Select((value, i) => (value, i: i + 1))
            .Where(pair => new[] { 20, 60, 100, 140, 180, 220 }.Contains(pair.i))
            .Sum(pair => pair.i * pair.value);
    }
}