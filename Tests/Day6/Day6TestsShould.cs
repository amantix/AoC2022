using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day6;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Day6;

public class Day6TestsShould
{
    private const string Prefix = "Day6";

    [TestCaseSource(nameof(GetPart1TestData))]
    public void TestPart1(string line, int expectedResult)
    {
        BlockFinder.FindFirstUniqueBlockIndex(line, 4).Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(GetPart2TestData))]
    public void TestPart2(string line, int expectedResult)
    {
        BlockFinder.FindFirstUniqueBlockIndex(line, 14).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetPart1TestData()
    {
        yield return new TestCaseData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7).SetName("Sample 1");
        yield return new TestCaseData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5).SetName("Sample 2");
        yield return new TestCaseData("nppdvjthqldpwncqszvftbrmjlhg", 6).SetName("Sample 3");
        yield return new TestCaseData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10).SetName("Sample 4");
        yield return new TestCaseData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11).SetName("Sample 5");
        var line = File.ReadAllLines(Path.Combine(Prefix, "input.txt")).SingleOrDefault(string.Empty);
        yield return new TestCaseData(line, 1804).SetName("Personal input");
    }

    private static IEnumerable<TestCaseData> GetPart2TestData()
    {
        yield return new TestCaseData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19).SetName("Sample 1");
        yield return new TestCaseData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23).SetName("Sample 2");
        yield return new TestCaseData("nppdvjthqldpwncqszvftbrmjlhg", 23).SetName("Sample 3");
        yield return new TestCaseData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29).SetName("Sample 4");
        yield return new TestCaseData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26).SetName("Sample 5");
        var line = File.ReadAllLines(Path.Combine(Prefix, "input.txt")).SingleOrDefault(string.Empty);
        yield return new TestCaseData(line, 2508).SetName("Personal input");
    }
}