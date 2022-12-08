using Common;

namespace Day7;

public class Part1Solver : ISolver<long>
{
    public long Solve(string[] lines)
    {
        var fileSystem = FileSystem.Restore(lines);
        return fileSystem.RootDirectory.Flatten().Select(directory => directory.Size).Where(size => size < 100_000).Sum();
    }
}