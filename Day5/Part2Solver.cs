using Common;

namespace Day5;

using static SupplyStacks;

public class Part2Solver : ISolver<string>
{
    public string Solve(string[] lines)
    {
        var stacks = InitializeStacks(lines);

        var commands = ExtractCommands(lines);

        static void CommandAction(IList<Stack<char>> supplyStacks, Command command)
        {
            var (count, from, to) = command;
            var temp = new Stack<char>();
            for (var i = 0; i < count; i++)
            {
                temp.Push(supplyStacks[from].Pop());
            }

            while (temp.Count > 0)
            {
                supplyStacks[to].Push(temp.Pop());
            }
        }

        stacks.ExecuteCommands(commands, CommandAction);

        return CombineTopCrates(stacks);
    }
}