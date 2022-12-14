using Common;

namespace Day14;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var map = RegolithReservoir.Create(lines, true);
        map.FillWithSand();
        return map.SandUnitsCount;
    }
}