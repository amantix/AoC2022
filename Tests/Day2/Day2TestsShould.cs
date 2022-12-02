using System.Collections.Generic;
using System.IO;
using Day2;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day2;

public class Day2TestsShould
{
    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver<RockPaperScissors.Moves>();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Solver<RockPaperScissors.Outcomes>();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine("Day2", "sample.txt"), 15).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day2", "input.txt"), 12458).SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine("Day2", "sample.txt"), 12).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day2", "input.txt"), 12683).SetName("Personal input");
    }
}