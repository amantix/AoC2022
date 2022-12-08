namespace Day8;

public static class TreeMatrixHelpers
{
    public static IEnumerable<(int, int)> FilterByMax(
        this IEnumerable<(int, int)> coordinates,
        string[] lines,
        int borderValue)
    {
        var max = borderValue;
        foreach (var (i, j) in coordinates)
        {
            if (lines[i][j] > max)
            {
                max = lines[i][j];
                yield return (i, j);
            }
        }
    }

    public static int CountScenicScore(string[] lines, int i, int j, Directions direction)
    {
        var count = 0;
        var limit =
            direction switch
            {
                Directions.Right => lines[0].Length,
                Directions.Down => lines.Length,
                _ => -1
            };
        var start = direction switch
        {
            Directions.Down => i + 1,
            Directions.Up => i - 1,
            Directions.Left => j - 1,
            Directions.Right => j + 1
        };
        var increment = direction is Directions.Down or Directions.Right ? 1 : -1;

        for (var k = start; k != limit; k += increment)
        {
            count++;
            if ((direction is Directions.Up or Directions.Down && lines[i][j] <= lines[k][j])
                || (direction is Directions.Left or Directions.Right && lines[i][j] <= lines[i][k]))
                break;
        }

        return count;
    }

    public enum Directions
    {
        Down,
        Up,
        Left,
        Right
    }
}