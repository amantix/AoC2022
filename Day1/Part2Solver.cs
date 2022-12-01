using Common;

namespace Day1;

public class Part2Solver: ISolver<int>
{
    public int Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var top = new PriorityQueue<int, int>();
        var current = 0;
        foreach (var line in lines)
        {
            if (int.TryParse(line, out var number))
            {
                current += number;
            }
            else
            {
                top.Enqueue(current, current);
                if (top.Count > 3)
                {
                    top.Dequeue();
                }
                current = 0;
            }
        }
        top.Enqueue(current, current);
        if (top.Count > 3)
        {
            top.Dequeue();
        }

        return top.UnorderedItems.Sum(x=>x.Element);
    }
}