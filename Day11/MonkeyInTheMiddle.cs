namespace Day11;

public class MonkeyInTheMiddle
{
    public record Monkey(
        int Id,
        List<long> Items,
        Func<long, long> Operation,
        int DivisibilityTest,
        int NextTrue,
        int NextFalse);

    public static List<Monkey> InitMonkeys(string[] lines)
    {
        var monkeys = new List<Monkey>();
        for (var i = 0; i < lines.Length; i += 7)
        {
            var items = lines[i + 1]
                .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
                .Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            var operationParts = lines[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Func<long, long> operation =
                operationParts[^2] switch
                {
                    "*" => operationParts[^1] switch
                    {
                        { } when int.TryParse(operationParts[^1], out var number) => old => checked(old * number),
                        "old" => old => checked(old * old),
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    "+" => operationParts[^1] switch
                    {
                        { } when int.TryParse(operationParts[^1], out var number) => old => old + number,
                        "old" => old => old + old,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    _ => throw new ArgumentOutOfRangeException()
                };
            var divisibilityTest = int.Parse(lines[i + 3].Split(' ', StringSplitOptions.RemoveEmptyEntries)[^1]);
            var nextTrue = int.Parse(lines[i + 4].Split(' ', StringSplitOptions.RemoveEmptyEntries)[^1]);
            var nextFalse = int.Parse(lines[i + 5].Split(' ', StringSplitOptions.RemoveEmptyEntries)[^1]);
            monkeys.Add(new Monkey(i / 7, items, operation, divisibilityTest, nextTrue, nextFalse));
        }

        return monkeys;
    }

    public static int[] GetCountersAfterSimulation(List<Monkey> monkeys, int roundsCount)
    {
        var counters = new int[monkeys.Count];
        var divisor = monkeys.Select(x => x.DivisibilityTest).Aggregate(1, (acc, x) => acc * x);
        for (var round = 0; round < roundsCount; round++)
        {
            foreach (var monkey in monkeys)
            {
                counters[monkey.Id] += monkey.Items.Count;

                foreach (var item in monkey.Items)
                {
                    var worryLevel = monkey.Operation(item);
                    if (worryLevel % monkey.DivisibilityTest == 0)
                    {
                        monkeys[monkey.NextTrue].Items.Add(worryLevel % divisor);
                    }
                    else
                    {
                        monkeys[monkey.NextFalse].Items.Add(worryLevel % divisor);
                    }
                }

                monkey.Items.Clear();
            }
        }

        return counters;
    }
}