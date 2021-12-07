public class Day7
{
    public static int Part1(string line)
    {
        var horizontalPositions = line.Split(",").Select(x => int.Parse(x)); ;
        var sorted = horizontalPositions.OrderBy(x => x).ToList();
        var mid = sorted.ElementAt(sorted.Count / 2); // :))

        return sorted.Sum(n => Math.Abs(mid - n));
    }

    public static int Part2(string line)
    {
        var positions = line.Split(",").Select(x => int.Parse(x))
        .OrderBy(x => x)
        .ToList();
        var matchPositions = Enumerable.Range(positions.First(), positions.Count + 1);
        var crabPositions = positions
        .GroupBy(pos => pos)
        .Select(grp => (pos: grp.Key, count: grp.Count()));

        return matchPositions.Aggregate(new List<int>(), (acc, matchPos) =>
       {
           var sum = crabPositions.Aggregate(0, (acc, crabPosition) =>
           {
               var (pos, count) = crabPosition;
               var distance = Math.Max(pos, matchPos) - Math.Min(pos, matchPos);
               var seriesSum = distance * (distance + 1) / 2;
               return acc + (seriesSum * count);
           });
           acc.Add(sum);
           return acc;
       })
       .Min();
    }
}