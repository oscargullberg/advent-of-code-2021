using System.Text;

public class Day14
{
    public static int Part1(IEnumerable<string> lines)
    {
        var polymerTemplate = lines.First().Trim();
        var pairInsertionRules = lines.Skip(2)
        .Select(line => line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
        .ToDictionary(x => x[0], x => x[1]);
        for (var i = 0; i < 10; i++)
        {
            polymerTemplate = BruteProcessStep(polymerTemplate, pairInsertionRules);
        }
        var charCounts = polymerTemplate
        .GroupBy(ch => ch)
        .ToDictionary(g => g.Key, g => g.Count());
        return charCounts.Max(kvp => kvp.Value) - charCounts.Min(kvp => kvp.Value);
    }

    public static long Part2(IEnumerable<string> lines)
    {
        var polymerTemplate = lines.First().Trim();
        var pairCounts = new Dictionary<string, long>();
        for (var i = 0; i < polymerTemplate.Length - 1; i++)
        {
            var pair = string.Concat(polymerTemplate[i], polymerTemplate[i + 1]);
            if (!pairCounts.TryAdd(pair, 1))
            {
                pairCounts[pair] += 1;
            }
        }

        var insertionRules = lines.Skip(2)
        .Select(line => line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
        .ToDictionary(x => x[0], x => x[1]);

        for (var i = 0; i < 40; i++)
        {
            ProcessStep(pairCounts, insertionRules);
        }

        var charCounts = pairCounts
        .Aggregate(new Dictionary<char, long>(), (acc, curr) =>
        {
            // Only taking first of each pair to avoid double counting
            // NNCB = (NN, NC, CB) == 3N,2C instead of 2N 1C 
            var first = curr.Key[0];
            if (!acc.TryAdd(first, curr.Value))
            {
                acc[first] += curr.Value;
            }
            return acc;
        });
        // And then increase the last from original polymer which isnt included in ^
        charCounts[polymerTemplate[^1]]++;

        return charCounts.Max(x => x.Value) - charCounts.Min(x => x.Value);
    }

    private static string BruteProcessStep(string polymerTemplate, Dictionary<string, string> insertionRules)
    {
        var insertions = new Dictionary<long, string>();
        for (var i = 0; i < polymerTemplate.Length - 1; i++)
        {
            var pair = string.Concat(polymerTemplate[i], polymerTemplate[i + 1]);
            if (insertionRules.ContainsKey(pair))
            {
                var insert = insertionRules[pair];
                insertions[i + 1] = insert;
            }
        }

        var templateBuilder = new StringBuilder();
        for (var i = 0; i < polymerTemplate.Length; i++)
        {
            if (insertions.ContainsKey(i))
            {
                templateBuilder.Append(insertions[i]);
            }
            templateBuilder.Append(polymerTemplate[i]);
        }
        return templateBuilder.ToString();
    }

    private static void ProcessStep(Dictionary<string, long> pairCounts, Dictionary<string, string> insertionRules)
    {
        var changes = new Dictionary<string, long>();
        foreach (var pair in pairCounts.Select(x => x.Key).Where(insertionRules.Keys.Contains))
        {
            var insertChar = insertionRules[pair];
            var left = pair[0] + insertChar;
            var right = insertChar + pair[1];

            if (!changes.TryAdd(pair, -pairCounts[pair]))
            {
                changes[pair] -= pairCounts[pair];
            }
            if (!changes.TryAdd(left, pairCounts[pair]))
            {
                changes[left] += pairCounts[pair];
            }
            if (!changes.TryAdd(right, pairCounts[pair]))
            {
                changes[right] += pairCounts[pair];
            }
        }
        foreach (var change in changes)
        {
            if (!pairCounts.TryAdd(change.Key, change.Value))
            {
                pairCounts[change.Key] += change.Value;
            }
        }
    }
}