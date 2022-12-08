using Common;

namespace Day8;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var visited = new int[lines.Length, lines[0].Length];
        for (var i = 0; i < lines.Length; i++)
        {
            visited[i, 0] = 1;
            visited[i, lines[0].Length - 1] = 1;
        }

        for (var j = 0; j < lines[0].Length; j++)
        {
            visited[0, j] = 1;
            visited[lines.Length - 1, j] = 1;
        }

        //left-right
        for (var i = 1; i < lines.Length - 1; i++)
        {
            var max = lines[i][0];
            for (var j = 1; j < lines[i].Length - 1; j++)
            {
                if (lines[i][j] > max)
                {
                    visited[i, j] = 1;
                }

                max = lines[i][j] > max ? lines[i][j] : max;
            }
        }

        //right-left
        for (var i = 1; i < lines.Length - 1; i++)
        {
            var max = lines[i][lines[i].Length - 1];
            for (var j = lines[i].Length - 2; j > 0; j--)
            {
                if (lines[i][j] > max)
                {
                    visited[i, j] = 1;
                }

                max = lines[i][j] > max ? lines[i][j] : max;
            }
        }

        //top-down
        for (var j = 1; j < lines[0].Length - 1; j++)
        {
            var max = lines[0][j];
            for (var i = 1; i < lines.Length - 1; i++)
            {
                if (lines[i][j] > max)
                {
                    visited[i, j] = 1;
                }

                max = lines[i][j] > max ? lines[i][j] : max;
            }
        }

        //bottom-up
        for (var j = 1; j < lines[0].Length - 1; j++)
        {
            var max = lines[^1][j];
            for (var i = lines.Length - 2; i > 0; i--)
            {
                if (lines[i][j] > max)
                {
                    visited[i, j] = 1;
                }

                max = lines[i][j] > max ? lines[i][j] : max;
            }
        }


        var count = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (visited[i, j] == 1)
                {
                    count++;
                }
            }
        }

        return count;
    }
}