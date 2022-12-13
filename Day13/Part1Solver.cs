using Common;

namespace Day13;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return Enumerable.Range(0, lines.Length / 3)
            .Where(i => PacketData.Value.Parse(lines[3 * i]).Compare(PacketData.Value.Parse(lines[3 * i + 1])) < 0)
            .Sum(i => i + 1);
    }
}