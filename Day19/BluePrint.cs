namespace Day19;

public record BluePrint(
    int Id,
    int OreRobotOreCost,
    int ClayRobotOreCost,
    int ObsidianRobotOreCost,
    int ObsidianRobotClayCost,
    int GeodeRobotOreCost,
    int GeodeRobotObsidianCost)
{
    public static BluePrint[] ParseBluePrints(string[] lines)
    {
        return lines
            .Select(line =>
                Create(
                    line
                        .Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(x => int.TryParse(x, out _))
                        .Select(int.Parse)
                        .ToArray()))
            .ToArray();
    }

    private static BluePrint Create(int[] values) =>
        new(
            values[0],
            values[1],
            values[2],
            values[3],
            values[4],
            values[5],
            values[6]
        );

    private int MaxOreCost =>
        new[] { OreRobotOreCost, ClayRobotOreCost, ObsidianRobotOreCost, GeodeRobotOreCost }.Max();

    public int GetMaxGeodeCount(BluePrint bluePrint, int timeLimit)
    {
        var visited = new HashSet<(int time, (int, int, int, int) resources, (int, int, int, int) robots)>();
        var startState = (timeLimit, (0, 0, 0, 0), (1, 0, 0, 0));
        var queue = new Queue<(
            int time,
            (int ore, int clay, int obsidian, int geodes) resources,
            (int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots) robots)>();
        queue.Enqueue(startState);

        var maxGeodesCount = 0;
        while (queue.Count > 0)
        {
            var (time, resources, robots) = queue.Dequeue();

            maxGeodesCount = Math.Max(resources.geodes, maxGeodesCount);

            if (time == 0)
            {
                continue;
            }

            //prune excessive robots (if their production level is greater than necessary)
            robots = robots with
            {
                oreRobots = Math.Min(robots.oreRobots, bluePrint.MaxOreCost),
                clayRobots = Math.Min(robots.clayRobots, bluePrint.ObsidianRobotClayCost),
                obsidianRobots = Math.Min(robots.obsidianRobots, bluePrint.GeodeRobotObsidianCost)
            };

            //prune excessive resources (more than enough resource to build the most expensive robot, requiring that resource)
            resources = resources with
            {
                ore = Math.Min(resources.ore, time * bluePrint.MaxOreCost - robots.oreRobots * (time - 1)),
                clay = Math.Min(resources.clay,
                    time * bluePrint.ObsidianRobotClayCost - robots.clayRobots * (time - 1)),
                obsidian = Math.Min(resources.obsidian,
                    time * bluePrint.GeodeRobotObsidianCost - robots.obsidianRobots * (time - 1))
            };

            if (visited.Contains((bluePrint.Id, resources, robots)))
            {
                continue;
            }

            visited.Add((bluePrint.Id, resources, robots));

            //collect resources
            var nextState = (
                time: time - 1,
                resources: (
                    ore: resources.ore + robots.oreRobots,
                    clay: resources.clay + robots.clayRobots,
                    obsidian: resources.obsidian + robots.obsidianRobots,
                    geodes: resources.geodes + robots.geodeRobots),
                robots);
            queue.Enqueue(nextState);

            //build ore collecting robot
            if (resources.ore >= bluePrint.OreRobotOreCost)
            {
                queue.Enqueue(nextState with
                {
                    resources = nextState.resources with
                    {
                        ore = nextState.resources.ore - bluePrint.OreRobotOreCost
                    },
                    robots = nextState.robots with { oreRobots = robots.oreRobots + 1 }
                });
            }

            //build clay collecting robot
            if (resources.ore >= bluePrint.ClayRobotOreCost)
            {
                queue.Enqueue(nextState with
                {
                    resources = nextState.resources with
                    {
                        ore = nextState.resources.ore - bluePrint.ClayRobotOreCost
                    },
                    robots = nextState.robots with { clayRobots = robots.clayRobots + 1 }
                });
            }

            //build obsidian collecting robot
            if (resources.ore >= bluePrint.ObsidianRobotOreCost && resources.clay >= bluePrint.ObsidianRobotClayCost)
            {
                queue.Enqueue(nextState with
                {
                    resources = nextState.resources with
                    {
                        ore = nextState.resources.ore - bluePrint.ObsidianRobotOreCost,
                        clay = nextState.resources.clay - bluePrint.ObsidianRobotClayCost
                    },
                    robots = nextState.robots with { obsidianRobots = robots.obsidianRobots + 1 }
                });
            }

            //build geode cracking robot
            if (resources.ore >= bluePrint.GeodeRobotOreCost && resources.obsidian >= bluePrint.GeodeRobotObsidianCost)
            {
                queue.Enqueue(nextState with
                {
                    resources = nextState.resources with
                    {
                        ore = nextState.resources.ore - bluePrint.GeodeRobotOreCost,
                        obsidian = nextState.resources.obsidian - bluePrint.GeodeRobotObsidianCost
                    },
                    robots = nextState.robots with { geodeRobots = robots.geodeRobots + 1 }
                });
            }
        }

        return maxGeodesCount;
    }
}