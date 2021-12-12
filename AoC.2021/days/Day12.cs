public class Day12
{
    public static int Part1(IEnumerable<string> lines)
    {
        var graph = CreateGraph(lines);
        var res = GetAllPaths("start", "end", graph);
        return res.Count;
    }

    public static int Part2(IEnumerable<string> lines)
    {
        var graph = CreateGraph(lines);
        var res = GetAllPaths("start", "end", graph, part2: true);
        return res.Count;
    }

    private static Dictionary<string, HashSet<string>> CreateGraph(IEnumerable<string> lines)
    =>
    lines.Select(line => line.Split("-"))
    .Select(splitted => (from: splitted[0], to: splitted[1]))
    .Aggregate(new Dictionary<string, HashSet<string>>(), (acc, edge) =>
     {
         if (!acc.ContainsKey(edge.from))
         {
             acc[edge.from] = new HashSet<string>();
         }
         if (!acc.ContainsKey(edge.to))
         {
             acc[edge.to] = new HashSet<string>();
         }
         acc[edge.from].Add(edge.to);
         acc[edge.to].Add(edge.from);
         return acc;
     });

    private static List<List<string>> GetAllPaths(string from, string to, Dictionary<string, HashSet<string>> graph, List<string>? path = null, bool part2 = false)
    {
        path ??= new List<string>();
        path.Add(from);

        if (from == to)
        {
            return new() { new(path) };
        }

        var connections = graph[from];
        var smallPathCounts = path
        .Where(p => p == p.ToLower())
        .GroupBy(s => s)
        .Select(x => new { x.Key, Count = x.Count() })
        .ToDictionary(x => x.Key, x => x.Count);

        return connections.Where(isValid).SelectMany(v => GetAllPaths(v, to, graph, new(path), part2)).ToList();

        bool isValid(string v)
        {
            if (!part2)
            {
                return !(v == v.ToLower() && path.Contains(v));
            }

            if (v == "start" && smallPathCounts.ContainsKey("start"))
            {
                return false;
            }
            else if (v == v.ToLower() && smallPathCounts.Any(v => v.Value == 2) && smallPathCounts.ContainsKey(v))
            {
                return false;
            }
            return true;
        }
    }
}