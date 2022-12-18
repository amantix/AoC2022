using Common;

namespace Day18;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var lavaDroplet = LavaDroplet.Create(lines);
        return lavaDroplet.ExteriorSurfaceArea;
    }
}