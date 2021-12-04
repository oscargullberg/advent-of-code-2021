public class Day1
{
    public static int Part1(IEnumerable<string> lines) =>
    lines.Select(line => int.Parse(line))
    .Aggregate((increaseCount: 0, prev: (int?)null), (acc, curr) =>
    {
        if (acc.prev.HasValue && curr > acc.prev)
        {
            acc.increaseCount++;
        }
        acc.prev = curr;
        return acc;
    }, res => res.increaseCount);

    public static int Part2(IEnumerable<string> lines)
    =>
        lines.Select(line => int.Parse(line))
        .Aggregate(new List<List<int>>(), (acc, curr) =>
        acc.Append(new List<int>())
        .Select((list, i) => i > acc.Count - 3 ? list.Append(curr).ToList() : list).ToList()
        )
        .Aggregate((increaseCount: 0, prev: (int?)null), (acc, curr) =>
       {
           if (acc.prev.HasValue && curr.Count == 3 && curr.Sum() > acc.prev)
           {
               acc.increaseCount++;
           };
           acc.prev = curr.Sum();
           return acc;
       }, res => res.increaseCount);
}