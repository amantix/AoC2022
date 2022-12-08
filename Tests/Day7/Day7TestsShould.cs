using System.Collections.Generic;
using System.IO;
using Day7;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day7;

public class Day7TestsShould
{
    private const string Prefix = "Day7";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part1Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part2Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 95437).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 1845346).SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 24933642).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 3636703).SetName("Personal input");
    }
}