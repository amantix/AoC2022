namespace Day12;

public static class MapExtensions
{
    public static IEnumerable<(int i, int j)> SelectPositions<T>(this T[][] map, Func<T, bool> predicate)
        => Enumerable.Range(0, map.Length)
            .SelectMany(i => Enumerable.Range(0, map[0].Length).Select(j => (i, j)))
            .Where(coordinate => predicate(map[coordinate.i][coordinate.j]));

    public static int CountPathLength<T>(
        this T[][] map,
        IEnumerable<(int row, int column)> initialPositions,
        IEnumerable<(int row, int column)> targetPositions,
        Func<(int, int), (int, int), bool> stepPredicate)
    {
        var targetPositionsMap = targetPositions.ToHashSet();
        var visited = new HashSet<(int, int)>();
        var queue = new Queue<(int, int, int)>();
        foreach (var position in initialPositions)
        {
            visited.Add(position);
            queue.Enqueue((position.row, position.column, 0));
        }

        while (queue.Count > 0)
        {
            var (row, column, stepsCount) = queue.Dequeue();
            if (targetPositionsMap.Contains((row, column)))
            {
                return stepsCount;
            }

            var nextCoordinates = new (int dRow, int dColumn)[] { (-1, 0), (1, 0), (0, -1), (0, 1) }
                .Select(shift => (row: row + shift.dRow, column: column + shift.dColumn))
                .Where(coordinate => coordinate.row >= 0
                                     && coordinate.row < map.Length
                                     && coordinate.column >= 0
                                     && coordinate.column < map[0].Length)
                .Where(coordinate => !visited.Contains(coordinate))
                .Where(coordinate => stepPredicate((row, column), coordinate));

            foreach (var (newRow, newColumn) in nextCoordinates)
            {
                queue.Enqueue((newRow, newColumn, stepsCount + 1));
                visited.Add((newRow, newColumn));
            }
        }

        return -1;
    }
}