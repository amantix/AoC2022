using System.Collections.Generic;
using System.IO;
using Day15;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day15;

[Parallelizable(ParallelScope.All)]
public class Day15TestsShould
{
    private const string Prefix = "Day15";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int lineNumber, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver();
        solver.SolvePart1(lines, lineNumber).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, int limit, long expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver();
        solver.SolvePart2(lines, limit).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 10, 26).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 2000000, 5688618).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 20, 56000011).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 4000000, 12625383204261).SetName("Personal input");
    }
}