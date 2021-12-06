public class Day5
{
    public static int Part1(IEnumerable<string> lines)
    => GetOverlappingCount(lines, segment => segment.x0 == segment.x1 || segment.y0 == segment.y1);

    public static int Part2(IEnumerable<string> lines)
    => GetOverlappingCount(lines);

    private static int GetOverlappingCount(IEnumerable<string> lines, Func<(int x0, int x1, int y0, int y1), bool>? segmentPredicate = null)
    =>
    lines.Select(ParseLineSegments)
    .Where(segmentPredicate ?? ((_) => true))
    .SelectMany(GetLines)
    .Aggregate(new Dictionary<(int, int), int>(), (acc, coordinate) =>
    {
        if (acc.ContainsKey(coordinate))
        {
            acc[coordinate]++;
        }
        else
        {
            acc.Add(coordinate, 1);
        }
        return acc;
    })
    .Where(m => m.Value >= 2)
    .Count();

    private static (int x0, int x1, int y0, int y1) ParseLineSegments(string line)
    =>
    line.Replace(" ", "")
    .Split("->")
    .SelectMany(coordString => coordString.Split(","))
    .Select(int.Parse)
    .Chunk(4)
    .Select(coords => (x0: coords[0], x1: coords[2], y0: coords[1], y1: coords[3]))
    .First();

    private static IEnumerable<(int x, int y)> GetLines((int x0, int x1, int y0, int y1) pair)
    {
        var (x0, x1, y0, y1) = pair;

        if (x0 == x1)
        {
            return Enumerable.Range(Math.Min(y0, y1), Math.Max(y1, y0) - Math.Min(y1, y0) + 1).Select(r => (x: x0, y: r));
        }
        else if (y1 == y0)
        {
            return Enumerable.Range(Math.Min(x0, x1), Math.Max(x0, x1) - Math.Min(x0, x1) + 1).Select(r => (x: r, y: y0));
        }
        else
        {
            return (x1 > x0 ? Enumerable.Range(x0, x1 - x0 + 1) : (Enumerable.Range(x1, x0 - x1 + 1)).Reverse())
            .Zip((y1 > y0 ? Enumerable.Range(y0, y1 - y0 + 1) : (Enumerable.Range(y1, y0 - y1 + 1)).Reverse()))
            .Select((x) => (x: x.First, y: x.Second));
        }
    }
}