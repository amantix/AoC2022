namespace Day7;

public class FileSystem
{
    public readonly Directory RootDirectory = new("/", null!);
    public record File(string Name, long Size);
    public record Directory(string Name, Directory Parent)
    {
        public readonly List<Directory> SubDirectories = new();
        public readonly List<File> Files = new();
        public long Size => Files.Sum(file => file.Size) + SubDirectories.Sum(directory => directory.Size);

        public IEnumerable<Directory> Flatten() =>
            SubDirectories.SelectMany(directory => directory.Flatten()).Prepend(this);
    }

    public static FileSystem Restore(string[] lines)
    {
        var fileSystem = new FileSystem();
        var currentDirectory = fileSystem.RootDirectory;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "$":
                    currentDirectory = parts[1] switch
                    {
                        "cd" => parts[2] switch
                        {
                            "/" => fileSystem.RootDirectory,
                            ".." => currentDirectory.Parent,
                            _ => currentDirectory.SubDirectories.Single(directory => directory.Name == parts[2])
                        },
                        _ => currentDirectory
                    };

                    break;
                case "dir":
                    currentDirectory.SubDirectories.Add(new Directory(parts[1], currentDirectory));
                    break;
                default:
                    currentDirectory.Files.Add(new File(parts[1], int.Parse(parts[0])));
                    break;
            }
        }
        return fileSystem;
    }
}