using System.Collections.Generic;
using System.IO;
using Day11;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day11;

public class Day11TestsShould
{
    private const string Prefix = "Day11";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, long expectedResult)
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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 10605).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 90294).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 2713310158).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 18170818354).SetName("Personal input");
    }
}