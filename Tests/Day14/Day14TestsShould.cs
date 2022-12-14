using System.Collections.Generic;
using System.IO;
using Day14;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day14;

public class Day14TestsShould
{
    private const string Prefix = "Day14";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var map = RegolithReservoir.Create(lines, false);
        map.FillWithSand();
        TestContext.Out.WriteLine(map);
        var solver = new Part1Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string inputFileName, int expectedResult)
    {
        var lines = File.ReadAllLines(inputFileName);
        var map = RegolithReservoir.Create(lines, true);
        map.FillWithSand();
        TestContext.Out.WriteLine(map);
        var solver = new Part2Solver();
        solver.Solve(lines).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 24).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 665).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 93).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 25434).SetName("Personal input");
    }
}