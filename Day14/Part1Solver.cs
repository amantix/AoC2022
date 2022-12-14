using Common;

namespace Day14;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var map = RegolithReservoir.Create(lines, false);
        map.FillWithSand();
        return map.SandUnitsCount;
    }
}