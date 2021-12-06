public class Day6
{
    public static long Part1(IEnumerable<string> lines)
    {
        var initialState = ParseInitialState(lines.First());
        return Enumerable.Range(0, 80)
        .Aggregate(initialState, (acc, _) => SimulateDay(acc))
        .Sum(c => c.Value);
    }

    public static long Part2(IEnumerable<string> lines)
    {
        var initialState = ParseInitialState(lines.First());
        return Enumerable.Range(0, 256)
       .Aggregate(initialState, (acc, _) => SimulateDay(acc))
       .Sum(g => g.Value);
    }

    private static Dictionary<int, long> ParseInitialState(string line)
    {
        var dict = line.Trim().Split(",").Select(int.Parse).ToList().GroupBy(k => k)
        .Select(g => new { Num = g.Key, Cnt = g.Count() })
        .ToDictionary(k => k.Num, v => (long)v.Cnt);

        return Enumerable.Range(0, 9).Aggregate(dict, (acc, key) =>
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = 0;
            }
            return dict;
        });
    }

    private static Dictionary<int, long> SimulateDay(Dictionary<int, long> timerCounts)
    {
        var initial = new Dictionary<int, long>(timerCounts);
        foreach (var timer in initial.Keys)
        {
            var count = initial[timer];
            if (timer > 0)
            {
                timerCounts[timer] -= count;
                timerCounts[timer - 1] += count;
            }
            else if (timer == 0)
            {
                timerCounts[0] -= count;
                timerCounts[6] += count;
                timerCounts[8] += count;
            }
        }
        return timerCounts;
    }
}