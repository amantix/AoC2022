using System.Collections.Generic;
using System.IO;
using Day20;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day20;

[Parallelizable(ParallelScope.All)]
public class Day20TestsShould
{
    private const string Prefix = "Day20";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, long expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver();
        solver.SolvePart1(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, long expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver();
        solver.SolvePart2(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 3).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 2215).SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 1623178306).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 8927480683).SetName("Personal input");
    }
}