using System.Diagnostics;
using Common;

namespace Day16;

public class Part1Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var valveMap = lines.Select(line =>
                line.Split(
                    new[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " },
                    StringSplitOptions.RemoveEmptyEntries)
            )
            .Select(lineParts => (valve: lineParts[0], flow: int.Parse(lineParts[1]),
                neighbors: lineParts[2].Split(", ", StringSplitOptions.RemoveEmptyEntries)))
            .ToDictionary(valveInfo => valveInfo.valve, valveInfo => (valveInfo.flow, valveInfo.neighbors));

        var paths = new Dictionary<string, Dictionary<string, int>>();

        foreach (var fromValve in valveMap.Keys)
        {
            var pathsFromValve = new Dictionary<string, int>();
            var queue = new Queue<(string valve, int pathLength)>();
            queue.Enqueue((fromValve, 0));
            while (queue.Count > 0)
            {
                var (valve, pathLength) = queue.Dequeue();

                foreach (var neighbor in valveMap[valve].neighbors)
                {
                    if (pathsFromValve.ContainsKey(neighbor))
                    {
                        continue;
                    }

                    pathsFromValve[neighbor] = pathLength + 1;
                    queue.Enqueue((neighbor, pathLength + 1));
                }
            }

            paths[fromValve] = pathsFromValve;
        }

        var usefulValves = valveMap.Where(valve => valve.Value.flow > 0).Select(valve => valve.Key).ToArray();
        return CountReleasedPressure("AA", 30, usefulValves);

        int CountReleasedPressure(string currentValve, int timeLeft, string[] usefulValves)
        {
            return usefulValves
                .Select(nextValve => (nextValve, newTime: timeLeft - paths[currentValve][nextValve] - 1))
                .Where(pair => pair.newTime > 0)
                .Select(pair
                    => pair.newTime * valveMap[pair.nextValve].flow
                       + CountReleasedPressure(pair.nextValve, pair.newTime,
                           usefulValves.Where(name => name != pair.nextValve).ToArray())
                )
                .Aggregate(0, Math.Max);
        }
    }
}

public class Part2Solver : ISolver<int>
{
    public int Solve(string[] lines)
    {
        var valveMap = lines.Select(line =>
                line.Split(
                    new[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " },
                    StringSplitOptions.RemoveEmptyEntries)
            )
            .Select(lineParts => (valve: lineParts[0], flow: int.Parse(lineParts[1]),
                neighbors: lineParts[2].Split(", ", StringSplitOptions.RemoveEmptyEntries)))
            .ToDictionary(valveInfo => valveInfo.valve, valveInfo => (valveInfo.flow, valveInfo.neighbors));

        var paths = new Dictionary<string, Dictionary<string, int>>();

        foreach (var fromValve in valveMap.Keys)
        {
            var pathsFromValve = new Dictionary<string, int>();
            var queue = new Queue<(string valve, int pathLength)>();
            queue.Enqueue((fromValve, 0));
            while (queue.Count > 0)
            {
                var (valve, pathLength) = queue.Dequeue();

                foreach (var neighbor in valveMap[valve].neighbors)
                {
                    if (pathsFromValve.ContainsKey(neighbor))
                    {
                        continue;
                    }

                    pathsFromValve[neighbor] = pathLength + 1;
                    queue.Enqueue((neighbor, pathLength + 1));
                }
            }

            paths[fromValve] = pathsFromValve;
        }

        var usefulValves = valveMap.Where(valve => valve.Value.flow > 0).Select(valve => valve.Key).ToArray();
        return CountReleasedPressureTogether(new[]{"AA","AA"},new[]{26,26}, usefulValves);

        int CountReleasedPressureTogether(string[]currentValves, int[]timeLeft, string[] usefulValves)
        {
            var best = 0;
            var actor = timeLeft[0] > timeLeft[1] ? 0 : 1;

            var valve = currentValves[actor];
            foreach (var nextValve in usefulValves)
            {
                var newTimeLeft = timeLeft[actor] - paths[valve][nextValve] - 1;
                if (newTimeLeft > 0)
                {
                    var newTimes = new[] { newTimeLeft, timeLeft[1 - actor] };
                    var newValves = new[] { nextValve, currentValves[1 - actor] };
                    var gain = newTimeLeft * valveMap[nextValve].flow
                               + CountReleasedPressureTogether(newValves, newTimes,
                                   usefulValves.Where(name => name != nextValve).ToArray());
                    if (gain > best)
                    {
                        best = gain;
                    }
                }
            }

            return best;
        }
    }
}