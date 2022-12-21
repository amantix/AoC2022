namespace Day21;

public class Solver
{
    public long SolvePart1(string[] lines)
    {
        var input = lines
            .Select(line => line.Split(new[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(parts => parts[0], parts => parts);
        var monkeys = CalculateMonkeyAnswers(input);
        return monkeys["root"];
    }
    public long SolvePart2(string[] lines)
    {
        var input = lines
            .Select(line => line.Split(new[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(parts => parts[0], parts => parts);
        var monkeys = CalculateMonkeyAnswers(input);

        var path = GetPathToMonkey(input, "humn");

        return ReverseCompute(input, monkeys, path);
    }

    private static long ReverseCompute(
        Dictionary<string, string[]> input,
        Dictionary<string, long> monkeyAnswers,
        List<string> pathToMonkey)
    {
        var line = input[pathToMonkey[0]];
        var result = input["root"][1] == pathToMonkey[0] 
            ? monkeyAnswers[input["root"][3]] 
            : monkeyAnswers[input["root"][1]];
        foreach(var current in pathToMonkey.Skip(1))
        {
            long partValue;
            bool leftNumber = false;
            
            if (line[1] == current)
            {
                partValue = monkeyAnswers[line[3]];
            }
            else
            {
                partValue = monkeyAnswers[line[1]];
                leftNumber = true;
            }

            result = line[2] switch
            {
                "+" => result - partValue,
                "-" => leftNumber ? partValue - result : result + partValue,
                "*" => result / partValue,
                "/" => result * partValue
            };
            line = input[current];
        }

        return result;
    }

    private static List<string> GetPathToMonkey(Dictionary<string, string[]> input, string monkey)
    {
        return Helper("root", new());

        List<string> Helper(string current, List<string> currentPath)
        {
            if (current == monkey)
            {
                return currentPath;
            }

            if (input[current].Length == 2)
            {
                return new();
            }

            var leftMonkey = input[current][1];
            var leftPath = Helper(leftMonkey, new List<string>(currentPath){leftMonkey});
            if (leftPath.Count > 0)
            {
                return leftPath;
            }

            var rightMonkey = input[current][3];
            return Helper(rightMonkey, new List<string>(currentPath) { rightMonkey });
        }
    }

    private static Dictionary<string, long> CalculateMonkeyAnswers(Dictionary<string, string[]> input)
    {
        var monkeys = new Dictionary<string, long>();
        CalculateMonkeyAnswer("root");
        return monkeys;
        
        void CalculateMonkeyAnswer(string monkey)
        {
            if (monkeys.ContainsKey(monkey))
            {
                return;
            }

            var parts = input[monkey];
            if (parts.Length == 2)
            {
                var value = int.Parse(parts[1]);
                monkeys[parts[0]] = value;
                return;
            }

            CalculateMonkeyAnswer(parts[1]);
            var left = monkeys[parts[1]];
            CalculateMonkeyAnswer(parts[3]);
            var right = monkeys[parts[3]];
            var result = parts[2] switch
            {
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => left / right,
                _ => throw new ArgumentOutOfRangeException()
            };
            monkeys[monkey] = result;
        }
    }
}