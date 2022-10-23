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

        var output = "";
        foreach (var y in Enumerable.Range(0, map.Max(m => m.Key.y) + 1))
        {
            output += Environment.NewLine;
            foreach (var x in Enumerable.Range(0, map.Max(m => m.Key.x) + 1))
            {
                output += map.ContainsKey((x, y)) ? "#" : ".";
            }
        }
        return output;
    }

    private static (Dictionary<(int x, int y), string> dotMap, List<(string axis, int coord)> foldInstructions) ParseInput(IEnumerable<string> lines)
    {
        var dotMap = lines.Where(l => l.Contains(','))
       .Select(line => line.Split(","))
       .Select(splitted => (x: int.Parse(splitted[0]), y: int.Parse(splitted[1])))
       .ToDictionary(x => x, _ => "#");

        var folds = lines.Where(l => l.StartsWith("fold"))
        .Select(l => l.Split(" ")[2].Split("="))
        .Select(x => (x[0], int.Parse(x[1])))
        .ToList();
        return (dotMap, folds);
    }

    private static void HandleFold((string axis, int coord) fold, Dictionary<(int x, int y), string> map)
    {
        var (axis, coord) = fold;
        // Fold left
        if (axis == "x")
        {
            var right = map.Where(k => k.Key.x > coord).ToList();
            foreach (var dot in right)
            {
                var dist = dot.Key.x - coord;
                map.TryAdd((coord - dist, dot.Key.y), "#");
                map.Remove(dot.Key);
            }
        }
        // Fold up 
        else
        {
            var below = map.Where(k => k.Key.y > coord).ToList();
            foreach (var dot in below)
            {
                var dist = dot.Key.y - coord;
                map.TryAdd((dot.Key.x, coord - dist), "#");
                map.Remove(dot.Key);
            }
        }
    }
}