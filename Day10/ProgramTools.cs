namespace Day10;

public static class ProgramTools
{
    public static IEnumerable<int> TraceProgram(string[] lines)
    {
        var currentValue = 1;
        yield return currentValue;
        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            yield return currentValue;
            if (parts[0] == "addx")
            {
                currentValue += int.Parse(parts[1]);
                yield return currentValue;
            }
        }
    }

    public static IEnumerable<string> SplitLines(string screen, int size = 40)
    {
        return Enumerable.Range(0, screen.Length/size).Select(i => screen[(i * size)..(i * size + size)]);
    }
}