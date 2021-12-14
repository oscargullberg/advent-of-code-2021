public class Day13
{
    public static int Part1(IEnumerable<string> lines)
    {
        var (map, folds) = ParseInput(lines);

        var fold = folds.First();
        HandleFold(fold, map);

        return map.Count;
    }

    public static string Part2(IEnumerable<string> lines)
    {

        var (map, folds) = ParseInput(lines);
        foreach (var fold in folds)
        {
            HandleFold(fold, map);
        }

        var maxX = map.Max(m => m.Key.x);
        var maxY = map.Max(m => m.Key.y);

        var output = "";
        foreach (var y in Enumerable.Range(0, maxY + 1))
        {
            output += Environment.NewLine;
            foreach (var x in Enumerable.Range(0, maxX + 1))
            {

                if (map.ContainsKey((x, y)))
                {
                    output += "#";
                }
                else
                {
                    output += ".";
                }
            }
        }
        // Todo parse
        return output;
    }

    private static (Dictionary<(int x, int y), string> map, List<string> folds) ParseInput(IEnumerable<string> lines)
    {
        var dots = lines.Where(l => l.Contains(','))
       .Select(line => line.Split(","))
       .Select(splitted => (x: int.Parse(splitted[0]), y: int.Parse(splitted[1])));
        var map = dots.Aggregate(new Dictionary<(int x, int y), string>(), (acc, dot) =>
        {
            acc.Add(dot, "#");
            return acc;
        });
        var folds = lines.Where(l => l.StartsWith("fold"))
        .Select(l => l.Split(" ")[2])
        .ToList();

        return (map, folds);
    }

    private static void HandleFold(string fold, Dictionary<(int x, int y), string> map)
    {
        // Fold left
        if (fold.StartsWith("x"))
        {
            var x = int.Parse(fold.Split("=")[1]);
            var right = map.Where(k => k.Key.x > x).ToList();
            foreach (var dot in right)
            {
                var dist = dot.Key.x - x;
                map.TryAdd((x - dist, dot.Key.y), "#");
            }
            foreach (var dot in right)
            {
                map.Remove(dot.Key);
            }
        }
        // Fold up 
        else
        {
            var y = int.Parse(fold.Split("=")[1]);
            var below = map.Where(k => k.Key.y > y).ToList();
            foreach (var dot in below)
            {
                var dist = dot.Key.y - y;
                map.TryAdd((dot.Key.x, y - dist), "#");
            }
            foreach (var dot in below)
            {
                map.Remove(dot.Key);
            }
        }
    }
}