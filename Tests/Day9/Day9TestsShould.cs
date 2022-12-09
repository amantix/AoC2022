using System.Collections.Generic;
using System.IO;
using Day9;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day9;

public class Day9TestsShould
{
    private const string Prefix = "Day9";

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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 13).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 6236).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample2.txt"), 36).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 2449).SetName("Personal input");
    }
}