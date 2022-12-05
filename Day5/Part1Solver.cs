using Common;

namespace Day5;

using static SupplyStacks;

public class Part1Solver : ISolver<string>
{
    public string Solve(string[] lines)
    {
        var stacks = InitializeStacks(lines);

        var commands = ExtractCommands(lines);

        static void CommandAction(IList<Stack<char>> supplyStacks, Command command)
        {
            var (count, from, to) = command;
            for (var i = 0; i < count; i++)
            {
                supplyStacks[to].Push(supplyStacks[from].Pop());
            }
        }

        stacks.ExecuteCommands(commands, CommandAction);

        return CombineTopCrates(stacks);
    }
}