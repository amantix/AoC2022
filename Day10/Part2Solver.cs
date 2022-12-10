using Common;

namespace Day10;

public class Part2Solver : ISolver<string>
{
    public string Solve(string[] lines)
    {
        var values = ProgramTools.TraceProgram(lines).ToArray();

        return string.Join("",
            Enumerable.Range(0, 240).Select(i => i % 40 >= values[i] - 1 && i % 40 <= values[i] - 1 + 2 ? '#' : ' '));
    }
}