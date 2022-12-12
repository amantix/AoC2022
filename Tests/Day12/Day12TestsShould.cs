using System.Collections.Generic;
using System.IO;
using Day12;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day12;

public class Day12TestsShould
{
    private const string Prefix = "Day12";

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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 31).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 420).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 29).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 414).SetName("Personal input");
    }
}