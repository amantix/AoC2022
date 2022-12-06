namespace Day6;

public static class BlockFinder
{
    public static int FindFirstUniqueBlockIndex(string line, int length)
    {
        return Enumerable.Range(length, line.Length - length)
            .FirstOrDefault(i => line[(i - length)..i].Distinct().Count() == length, -1);
    }
}