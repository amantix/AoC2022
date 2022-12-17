using System.Diagnostics;
using Common;

namespace Day17;

public class Part2Solver : ISolver<long>
{
    public long Solve(string[] lines)
    {
        var chamber = Chamber.Simulate(lines[0], 10000);
        Debug.WriteLine(string.Join(", ", chamber.HeightLog));

        var deltas = new List<int>();
        for (var i = 0; i < chamber.HeightLog.Count; i++)
        {
            if (i == 0)
            {
                deltas.Add(chamber.HeightLog[i]);
            }
            else
            {
                deltas.Add(chamber.HeightLog[i] - chamber.HeightLog[i-1]);
            }
        }
        Debug.WriteLine(string.Join(", ", deltas));

        //prefix length = 15, prefix height = 25
        //period length = 35, period sum = 78 - 25 = 53
        //85 => 78 + 53 = 131
        
        //25 + 28 571 428 571 * 53 = 25 + 1 514 285 714 263 = 1 514 285 714 288
        
        
        //period length = 1740, period sum = 2716, period count = (1000000000000 - prefix length) div period length
        //prefix length = 118, prefix sum = 181
        //suffix length = 1062, suffix sum = 1676
        //total = prefix sum + suffix sum + period count * period sum
        
        //181 + 574 712 643 * 2716 + (1062)
        //181 + 1560919538388 + 1676 = 1560919540245
        return 0;
    }
}