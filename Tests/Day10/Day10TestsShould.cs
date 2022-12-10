using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Day10;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day10;

public class Day10TestsShould
{
    private const string Prefix = "Day10";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string inputFileName, int expectedResult)
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
        var output = solver.Solve(lines);
        Debug.WriteLine(string.Join('\n', ProgramTools.SplitLines(output)));
        output.Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), 13140).SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), 15880).SetName("Personal input");
    } 

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData(Path.Combine(Prefix, "sample.txt"), "##  ##  ##  ##  ##  ##  ##  ##  ##  ##  ###   ###   ###   ###   ###   ###   ### ####    ####    ####    ####    ####    #####     #####     #####     #####     ######      ######      ######      ###########       #######       #######     ").SetName("Sample");
        yield return new TestCaseData(Path.Combine(Prefix, "input.txt"), "###  #     ##  #### #  #  ##  ####  ##  #  # #    #  # #    # #  #  #    # #  # #  # #    #    ###  ##   #  #   #  #    ###  #    # ## #    # #  ####  #   # ## #    #    #  # #    # #  #  # #    #  # #    ####  ### #    #  # #  # ####  ### ").SetName("Personal input");
    }
}