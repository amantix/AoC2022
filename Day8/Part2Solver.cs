using Common;

namespace Day8;

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return Enumerable.Range(1, lines.Length - 1)
            .SelectMany(i => Enumerable.Range(1, lines[0].Length - 1).Select(j => (i, j)))
            .Max(coordinates => Enum.GetValues<Directions>()
                .Select(direction => CountScenicScore(coordinates.i, coordinates.j, direction))
                .Aggregate(1, (acc, x) => acc * x));

        int CountScenicScore(int i, int j, Directions direction)
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
    }

    enum Directions
    {
        Down,
        Up,
        Left,
        Right
    }
}