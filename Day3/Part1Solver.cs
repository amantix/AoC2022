using Common;

namespace Day3;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        return lines.Sum(line =>
            line[..(line.Length / 2)]
                .Intersect(line[(line.Length / 2)..])
                .Select(letter => char.IsLower(letter) ? letter - 'a' + 1 : letter - 'A' + 27)
                .Single());
    }
}