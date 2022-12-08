using Common;

namespace Day7;

public class Part2Solver : ISolver<long>
{
    public long Solve(string[] lines)
    {
        var fileSystem = FileSystem.Restore(lines);
        var directorySizes = fileSystem.RootDirectory.Flatten().Select(directory => directory.Size).ToArray();
        var spaceToFree = directorySizes[0] - 40_000_000;
        return directorySizes.Where(size => size > spaceToFree).Min();
    }
}