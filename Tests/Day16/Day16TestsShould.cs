using System.Collections.Generic;
using System.IO;
using Day16;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day16;

public class Day16TestsShould
{
    private const string Prefix = "Day16";

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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 1651).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 1775).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 1707).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 2351).SetName("Personal input");
    }
}