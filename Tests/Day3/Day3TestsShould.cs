using System.Collections.Generic;
using System.IO;
using Day3;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day3;

public class Day3TestsShould
{
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
        yield return new TestCaseData(Path.Combine("Day3", "sample.txt"), 157).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day3", "input.txt"), 7980).SetName("Personal input");
    }
    
    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine("Day3", "sample.txt"), 70).SetName("Sample");
        yield return new TestCaseData(Path.Combine("Day3", "input.txt"), 2881).SetName("Personal input");
    }
}