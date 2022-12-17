using System.Collections.Generic;
using System.IO;
using Day17;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day17;

[Parallelizable(ParallelScope.All)]
public class Day15TestsShould
{
    private const string Prefix = "Day17";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part1Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, long expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part2Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 3068).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 3151).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 1514285714288).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 1560919540245).SetName("Personal input");
    }
}