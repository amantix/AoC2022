using Common;

namespace Day13;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        const string divider2 = "[[2]]";
        const string divider6 = "[[6]]";

        var orderedPacketData = lines
            .Where(line => !string.IsNullOrEmpty(line))
            .Append(divider2)
            .Append(divider6)
            .Select(line => (line, value: PacketData.Value.Parse(line)))
            .OrderBy(tuple => tuple.value, new PacketData.Value.ValueComparer())
            .Select(tuple => tuple.line)
            .ToArray();

        var divider2Index = Array.IndexOf(orderedPacketData, divider2) + 1;
        var divider6Index = Array.IndexOf(orderedPacketData, divider6) + 1;
        return divider2Index * divider6Index;
    }
}