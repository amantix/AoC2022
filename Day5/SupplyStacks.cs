namespace Day5;

public static class SupplyStacks
{
    public static IList<Stack<char>> InitializeStacks(IList<string> lines)
    {
        return lines
            .TakeWhile(x => !string.IsNullOrEmpty(x) && !x.StartsWith(" 1"))
            .Aggregate(Enumerable.Range(0, (lines.First().Length + 1) / 4).Select(_ => new Stack<char>()).ToArray(),
                (acc, line) =>
                {
                    foreach (var (letter, index)
                             in line
                                 .Select((symbol, index) => (symbol, index / 4))
                                 .Where(pair => char.IsLetter(pair.symbol))
                            )
                    {
                        acc[index].Push(letter);
                    }

                    return acc;
                })
            .Select(stack => new Stack<char>(stack))
            .ToArray();
    }

    public record struct Command(int Count, int From, int To);

    public static IList<Command> ExtractCommands(IEnumerable<string> lines)
    {
        return lines
            .SkipWhile(x => !x.StartsWith("move"))
            .Select(line => line.Split(new[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray())
            .Select(values => new Command(values[0], values[1] - 1, values[2] - 1))
            .ToArray();
    }

    public static void ExecuteCommands(
        this IList<Stack<char>> stacks,
        IEnumerable<Command> commands,
        Action<IList<Stack<char>>, Command> commandAction)
    {
        foreach (var command in commands)
        {
            commandAction(stacks, command);
        }
    }

    public static string CombineTopCrates(IEnumerable<Stack<char>> stacks)
    {
        return string.Join("", stacks.Where(stack => stack.Count > 0).Select(stack => stack.Peek()));
    }
}