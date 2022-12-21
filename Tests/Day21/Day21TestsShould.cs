using System.Collections.Generic;
using System.IO;
using Day21;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day21;

[Parallelizable(ParallelScope.All)]
public class Day21TestsShould
{
    private const string Prefix = "Day21";

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
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 152).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 51928383302238).SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 301).SetName("Sample");
        //9584437937672 too high
        //3305669217840
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 3305669217840).SetName("Personal input");
    }
}