using System.Collections.Generic;
using System.IO;
using Day5;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day5;

public class Day5TestsShould
{
    private const string Prefix = "Day5";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, string expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part1Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, string expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var solver = new Part2Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), "CMZ").SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), "SPFMVDTZT").SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), "MCD").SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), "ZFSJBPRFP").SetName("Personal input");
    }
}