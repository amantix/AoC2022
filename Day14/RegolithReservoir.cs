namespace Day14;

public class RegolithReservoir
{
    private RegolithReservoir() {}
    private Dictionary<(int x, int y), char> map = new();
    private bool hasFloor;
    private int? sandUnitsCount;
    public static RegolithReservoir Create(string[] lines, bool hasFloor)
    {
        var paths = ParsePaths(lines);
        var reservoir = new RegolithReservoir
        {
            map = CreateMap(paths),
            hasFloor = hasFloor
        };
        return reservoir;
    }

    public void FillWithSand()
    {
        var floorLevel = map.Keys.Max(coordinate => coordinate.y) + 1;
        (int x, int y) unit;
        while (!new[] { (500, 0), (-1, -1) }.Contains(unit = LocateRest(map, floorLevel, hasFloor)))
        {
            map[(unit.x, unit.y)] = 'o';
        }

        if (unit == (500, 0))
        {
            map[unit] = 'o';
        }

        sandUnitsCount = null;
    }

    public void Clear()
    {
        foreach (var position in map.Keys.Where(position => map[position] == 'o'))
        {
            map.Remove(position);
        }

        sandUnitsCount = null;
    }

    public int SandUnitsCount => sandUnitsCount ??= map.Count(pair => pair.Value == 'o');

    public override string ToString()
    {
        var (minX, minY, maxX, maxY) = map.Keys
            .Aggregate((minX: int.MaxValue, minY: int.MaxValue, maxX: int.MinValue, maxY: int.MinValue),
                (acc, point) => (
                    Math.Min(acc.minX, point.x),
                    Math.Min(acc.minY, point.y),
                    Math.Max(acc.maxX, point.x),
                    Math.Max(acc.maxY, point.y))
            );
        var lines = string.Join("\n", Enumerable.Range(minY, maxY - minY + 1)
            .Select(y => string.Join(string.Empty,
                Enumerable.Range(minX, maxX - minX + 1)
                    .Select(x => map.TryGetValue((x, y), out var value) ? value : '.'))
            )
        );

        if (hasFloor)
        {
            lines += $"\n{new string('#', maxX - minX + 1)}";
        }

        return lines;
    }

    private static (int x, int y)[][] ParsePaths(string[] lines)
    {
        var paths = lines
            .Select(line =>
                line
                    .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(part
                        => part.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse).ToArray())
                    .Select(array => (x: array[0], y: array[1]))
                    .ToArray()
            )
            .ToArray();
        return paths;
    }


    private static Dictionary<(int x, int y), char> CreateMap((int x, int y)[][] paths)
    {
        var map = new Dictionary<(int x, int y), char>();
        foreach (var path in paths)
        {
            for (var i = 1; i < path.Length; i++)
            {
                var (dx, dy) = (Math.Sign(path[i].x - path[i - 1].x), Math.Sign(path[i].y - path[i - 1].y));
                var point = path[i];
                map[(point.x, point.y)] = '#';
                do
                {
                    point = (point.x - dx, point.y - dy);
                    map[(point.x, point.y)] = '#';
                } while (point != path[i - 1]);
            }
        }

        map[(500, 0)] = '+';
        return map;
    }

    private static (int x, int y) LocateRest(Dictionary<(int x, int y), char> map, int heightLimit, bool mapHasFloor)
    {
        var (x, y) = (500, 0);
        var rest = false;
        while (!rest)
        {
            if (y == heightLimit)
            {
                rest = true;
                if (!mapHasFloor)
                {
                    (x, y) = (-1, -1);
                }

                continue;
            }

            if (!map.ContainsKey((x, y + 1)))
            {
                y++;
            }
            else if (!map.ContainsKey((x - 1, y + 1)))
            {
                y++;
                x--;
            }
            else if (!map.ContainsKey((x + 1, y + 1)))
            {
                y++;
                x++;
            }
            else
            {
                rest = true;
            }
        }

        return (x, y);
    }
}