using System.Collections.Generic;
using System.IO;
using Day18;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day18;

[Parallelizable(ParallelScope.All)]
public class Day18TestsShould
{
    private const string Prefix = "Day18";

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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 64).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 4308).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 58).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 2540).SetName("Personal input");
    }
}