using Common;

namespace Day1;

public class Part1Solver: ISolver<int>
{
    public int Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var max = 0;
        var current = 0;
        foreach (var line in lines)
        {
            if (int.TryParse(line, out var number))
            {
                current += number;
            }
            else
            {
                max = Math.Max(max, current);
                current = 0;
            }
        }

        return Math.Max(max, current);
    }
}