namespace Day15;

public class Solver
{
    public int SolvePart1(string[] lines, int lineIndex)
    {
        var sensorsReport = ParseSensorsReport(lines);
        var coverage = BuildCoverage(sensorsReport, lineIndex, lineIndex);

        return coverage[lineIndex]
                   .SelectMany(range => Enumerable.Range(range.from, range.to - range.from + 1))
                   .Distinct()
                   .Count()
               - sensorsReport.SelectMany(pair => new[] { pair.sensor, pair.beacon })
                   .Distinct()
                   .Count(pair => pair.y == lineIndex);
    }

    public long SolvePart2(string[] lines, int limit)
    {
        var sensorsReport = ParseSensorsReport(lines);
        var coverage = BuildCoverage(sensorsReport, 0, limit);

        var gap = coverage
            .Select(line => (containsGap: TryFindGap(line.Value, out var x), x: x, y: line.Key))
            .First(line => line.containsGap);

        return gap.x * 4000000L + gap.y;
    }

    private static bool TryFindGap(IEnumerable<(int from, int to)> ranges, out int x)
    {
        var currentRange = (from: int.MaxValue, to: int.MinValue);
        foreach (var range in ranges.OrderBy(pair => pair))
        {
            if (currentRange.to == int.MinValue || currentRange.to >= range.from - 1)
            {
                currentRange = (Math.Min(currentRange.from, range.from), Math.Max(currentRange.to, range.to));
            }
            else
            {
                x = currentRange.to + 1;
                return true;
            }
        }

        x = int.MinValue;
        return false;
    }

    private static Dictionary<int, List<(int from, int to)>> BuildCoverage(
        IEnumerable<((int x, int y) sensor, (int x, int y) beacon, int distance)> sensorsReport,
        int lowerBound,
        int upperBound)
    {
        var coverage = new Dictionary<int, List<(int from, int to)>>();
        foreach (var (sensor, _, distance) in sensorsReport)
        {
            var from = Math.Max(lowerBound, sensor.y - distance);
            var to = Math.Min(upperBound, sensor.y + distance);
            for (var y = from; y <= to; y++)
            {
                if (!coverage.ContainsKey(y))
                {
                    coverage[y] = new();
                }

                var delta = distance - Math.Abs(y - sensor.y);
                coverage[y].Add((sensor.x - delta, sensor.x + delta));
            }
        }

        return coverage;
    }

    private static ((int x, int y) sensor, (int x, int y) beacon, int distance)[] ParseSensorsReport(
        IEnumerable<string> lines)
    {
        var sensorsReport = lines
            .Select(line => line
                .Split(",=:".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(part => int.TryParse(part, out _))
                .Select(int.Parse)
                .ToArray())
            .Select(positions =>
                (sensor: (x: positions[0], y: positions[1]), beacon: (x: positions[2], y: positions[3]),
                    distance: Math.Abs(positions[0] - positions[2]) + Math.Abs(positions[1] - positions[3])))
            .ToArray();
        return sensorsReport;
    }
}