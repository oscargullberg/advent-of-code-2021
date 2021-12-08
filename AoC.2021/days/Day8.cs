public class Day8
{
    public static int Part1(IEnumerable<string> lines)
    {
        List<int> lengthsOfInterest = new()
        {
            2, // 1
            4, // 4
            3, // 7
            7  // 8
        };

        return lines.Select(line => line.Split("|").ElementAt(1))
        .SelectMany(line => line.Trim().Split(" "))
        .Count(outputValue => lengthsOfInterest.Contains(outputValue.Length));
    }

    public static int Part2(IEnumerable<string> lines)
    {
        var numPatternLengths = new Dictionary<int, int>()
        {
            { 2, 1}, // 1
            { 4,4},  // 4
            { 3,7},  // 7
            { 7,8}   // 8
        };

        var signalTranslations =
        lines
        .Select(line => line.Split("|").ElementAt(0))
        .Select(line => line.Trim().Split(" "))
        .Select(signals =>
        {
            var known = new Dictionary<string, int>() { };
            var unknown = new HashSet<string>();
            foreach (var signal in signals.Select(signal => string.Join("", signal.OrderBy(x => x))))
            {
                if (numPatternLengths.ContainsKey(signal.Length))
                {
                    known[signal] = numPatternLengths[signal.Length];
                }
                else
                {
                    unknown.Add(signal);
                }
            }
            return (known: known, unknown: unknown);
        })
        .Select(signalData =>
        {
            var (known, unknown) = signalData;
            var four = known.First(kvp => kvp.Value == 4).Key;
            var one = known.First(kvp => kvp.Value == 1).Key;

            var fivers = unknown.Where(v => v.Length == 5).ToList();
            var three = fivers.Where(v => one.All(c => v.Contains(c))).First();
            var two = fivers.Where(v => v != three).Single(v => v.Intersect(four).Count() == 2);
            var five = fivers.Except(new string[] { three, two }).Single();

            var sixers = unknown.Where(v => v.Length == 6).ToList();
            var six = sixers.Single(u => u.Intersect(one).Count() == 1);
            var zero = sixers.Where(v => v != six).Single(u => u.Intersect(four).Count() == 3);
            var nine = sixers.Except(new[] { six, zero }).Single();

            known.Add(two, 2);
            known.Add(three, 3);
            known.Add(five, 5);
            known.Add(six, 6);
            known.Add(nine, 9);
            known.Add(zero, 0);
            return known;
        })
        .ToList();

        return lines
        .Select(line => line.Split("|").ElementAt(1))
        .Select((line, i) => line.Trim().Split(" ").Select(output => string.Join("", output.OrderBy(x => x))))
        .Select((outputs, i) => outputs.Select(output => signalTranslations[i][output]).Aggregate("", (acc, v) => acc + v))
        .Sum(int.Parse);
    }
}