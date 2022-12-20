namespace Day20;

public class Solver
{
    public long SolvePart1(string[] lines)
    {
        var values = lines
            .Select(long.Parse)
            .Select((value, index) => (value, index))
            .ToList();
        var originalValues = values.ToArray();

        Mix(originalValues, values);

        return GroveCoordinatesSum(values);
    }

    public long SolvePart2(string[] lines)
    {
        var values = lines
            .Select(long.Parse)
            .Select((value, index) => (value: value * 811589153, index))
            .ToList();
        var originalValues = values.ToArray();

        for (var i = 0; i < 10; i++)
        {
            Mix(originalValues, values);
        }

        return GroveCoordinatesSum(values);
    }

    private static void Mix(IEnumerable<(long value, int index)> originalValues,
        IList<(long value, int index)> valuesToMix)
    {
        foreach (var pair in originalValues)
        {
            var oldIndex = valuesToMix.IndexOf(pair);
            var period = valuesToMix.Count - 1;
            var newIndex = (int)((oldIndex + pair.value) % period + period) % period;
            valuesToMix.RemoveAt(oldIndex);
            valuesToMix.Insert(newIndex, pair);
        }
    }

    private static long GroveCoordinatesSum(List<(long value, int index)> values)
        => GetCircularFromZero(values, 1000)
           + GetCircularFromZero(values, 2000)
           + GetCircularFromZero(values, 3000);

    private static long GetCircularFromZero(List<(long value, int index)> values, int index)
    {
        var zeroIndex = values.IndexOf(values.Find(x => x.value == 0));
        return values[(zeroIndex + index) % values.Count].value;
    }
}