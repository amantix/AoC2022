namespace Day18;

public class LavaDroplet
{
    private LavaDroplet()
    {
    }

    public static LavaDroplet Create(string[] lines)
    {
        var coordinates = ParseCoordinates(lines);
        var lavaDroplet = new LavaDroplet
        {
            cubeCoordinates = CreateMap(coordinates),
            innerCoordinates = CreateMap(GetInnerCoordinates(coordinates))
        };
        return lavaDroplet;
    }

    public int SurfaceArea => cubeCoordinates?.Sum(x => x.Value) ?? 0;
    public int ExteriorSurfaceArea => SurfaceArea - innerCoordinates?.Sum(x => x.Value) ?? 0;

    private Dictionary<(int, int, int), int>? cubeCoordinates;
    private Dictionary<(int, int, int), int>? innerCoordinates;

    private static (int i, int j, int k)[] ParseCoordinates(string[] lines)
    {
        return lines.Select(line
                => line
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray()
            )
            .Select(triple => (i: triple[0], j: triple[1], k: triple[2]))
            .ToArray();
    }

    private static (int i, int j, int k)[] GetInnerCoordinates(IEnumerable<(int i, int j, int k)> coordinates)
    {
        var hull = Enumerable.Range(0, 23)
            .SelectMany(i => Enumerable.Range(0, 23).SelectMany(j => Enumerable.Range(0, 23).Select(k => (i, j, k))))
            .Except(coordinates)
            .ToHashSet();
        var stack = new Stack<(int, int, int)>();
        hull.Remove(default);
        stack.Push(default);
        while (stack.Count > 0)
        {
            var (i, j, k) = stack.Pop();
            var deltas = new[] { (-1, 0, 0), (1, 0, 0), (0, -1, 0), (0, 1, 0), (0, 0, -1), (0, 0, 1) };
            var neighborCoordinates = deltas
                .Select(triple => (i: i + triple.Item1, j: j + triple.Item2, k: k + triple.Item3))
                .Where(hull.Contains)
                .ToArray();
            foreach (var coordinate in neighborCoordinates)
            {
                hull.Remove(coordinate);
                stack.Push(coordinate);
            }
        }

        return hull.ToArray();
    }

    private static Dictionary<(int, int, int), int> CreateMap((int i, int j, int k)[] coordinates)
    {
        var map = coordinates.ToDictionary(coordinate => coordinate, _ => 0);

        var visited = new HashSet<(int i, int j, int k)>();

        foreach (var start in coordinates.Where(coordinate => !visited.Contains(coordinate)))
        {
            var stack = new Stack<(int i, int j, int k)>();
            stack.Push(start);
            visited.Add(stack.Peek());

            while (stack.Count > 0)
            {
                var (i, j, k) = stack.Pop();
                var deltas = new[] { (-1, 0, 0), (1, 0, 0), (0, -1, 0), (0, 1, 0), (0, 0, -1), (0, 0, 1) };

                var neighborCoordinates = deltas
                    .Select(triple => (i: i + triple.Item1, j: j + triple.Item2, k: k + triple.Item3))
                    .Where(map.ContainsKey)
                    .ToArray();
                map[(i, j, k)] = 6 - neighborCoordinates.Length;
                foreach (var coordinate in neighborCoordinates.Where(x => !visited.Contains(x)))
                {
                    visited.Add(coordinate);
                    stack.Push(coordinate);
                }
            }
        }

        return map;
    }
}