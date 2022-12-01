using System.Collections.Generic;
using System.IO;
using Day1;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day1;

public class Day1TestsShould
{
    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
    {
        File.ReadAllLines(inputFileName);
        var solver = new Part1Solver();
        solver.Solve(inputFileName).Should().Be(expectedResult);
    }
    
    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, int expectedResult)
    {
        File.ReadAllLines(inputFileName);
        var solver = new Part2Solver();
        solver.Solve(inputFileName).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine("Day1", "sample.txt"), 24000).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day1", "input.txt"), 67622).SetName("Personal input");
    }
    
    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine("Day1", "sample.txt"), 45000).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day1", "input.txt"), 201491).SetName("Personal input");
    }
}