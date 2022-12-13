using Common;

namespace Day13;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        const string divider2 = "[[2]]";
        const string divider6 = "[[6]]";
        lines = lines.Where(line => !string.IsNullOrEmpty(line)).Append(divider2).Append(divider6).ToArray();
        Array.Sort(lines, (left, right) => PacketData.Value.Parse(left).Compare(PacketData.Value.Parse(right)));

        var divider2Index = Array.IndexOf(lines, divider2) + 1;
        var divider6Index = Array.IndexOf(lines, divider6) + 1;
        return divider2Index * divider6Index;
    }
}