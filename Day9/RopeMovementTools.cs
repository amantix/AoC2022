namespace Day9;

public static class RopeMovementTools
{
    public static IEnumerable<(string direction, int movesCount)> ParseMovements(this string[] lines)
        => lines
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => (parts[0], int.Parse(parts[1])));

    public static IEnumerable<(int i, int j)> GetTailPath(
        IEnumerable<(string direction, int movesCount)> movements,
        int ropeLength)
    {
        var rope = new (int i, int j)[ropeLength];
        yield return rope.Last();

        foreach (var (direction, movementsCount) in movements)
        {
            for (var step = 0; step < movementsCount; step++)
            {
                rope[0] = rope[0].Shift(direction.GetMove());
                for (var knotIndex = 1; knotIndex < ropeLength; knotIndex++)
                {
                    var (di, dj) = GetDiff(rope[knotIndex - 1], rope[knotIndex]);

                    if (ShiftLength((di, dj)) > 1)
                    {
                        rope[knotIndex] = rope[knotIndex].Shift((Math.Sign(di), Math.Sign(dj)));
                    }
                }

                yield return rope.Last();
            }
        }
    }

    private static (int i, int j) Shift(this (int i, int j) position, (int di, int dj) shift)
        => (position.i + shift.di, position.j + shift.dj);

    private static (int di, int dj) GetDiff((int i, int j) from, (int i, int j) to)
        => (from.i - to.i, from.j - to.j);

    private static int ShiftLength((int di, int dj) shift) => Math.Max(Math.Abs(shift.di), Math.Abs(shift.dj));

    private static (int di, int dj) GetMove(this string direction)
    {
        return direction switch
        {
            "R" => (0, 1),
            "U" => (1, 0),
            "L" => (0, -1),
            "D" => (-1, 0),
            _ => (0, 0)
        };
    }
}