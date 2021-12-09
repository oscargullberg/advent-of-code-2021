public class Day9
{
    public static int Part1(IEnumerable<string> lines)
    {
        var (points, rowLength) = GetPoints(lines);
        var lowPoints = GetLowPointIndexes(points, rowLength);
        return lowPoints.Sum(i => points[i] + 1);
    }

    public static int Part2(IEnumerable<string> lines)
    {
        var (points, rowLength) = GetPoints(lines);
        var lowPoints = GetLowPointIndexes(points, rowLength);

        return lowPoints.Select(pos => GetBasinSum(points, pos, rowLength, new HashSet<int>()))
        .OrderByDescending(s => s).Take(3)
        .Aggregate(1, (acc, curr) => acc * curr);
    }

    private static (int[] points, int rowLength) GetPoints(IEnumerable<string> lines)
    {
        var rowLength = lines.First().Length;
        var points = lines
        .SelectMany(line => line.ToCharArray())
        .Select(ch => (int)char.GetNumericValue(ch))
        .ToArray();
        return (points, rowLength);
    }

    private static List<int> GetLowPointIndexes(int[] positions, int rowLength)
    {
        var lowPoints = new List<int>();
        for (var i = 0; i < positions.Length; i++)
        {
            var adjacent = GetAdjacentIndexes(positions, i, rowLength);
            if (adjacent.All(p => positions[p] > positions[i]))
            {
                lowPoints.Add(i);
            }
        }
        return lowPoints;
    }

    private static int GetBasinSum(int[] points, int index, int rowLength, HashSet<int> visited)
    {
        if (visited.Contains(index))
        {
            return 0;
        }
        visited.Add(index);

        var val = points[index];
        if (val == 9)
        {
            return 0;
        }

        var adjacentPositions = GetAdjacentIndexes(points, index, rowLength);
        return adjacentPositions.Aggregate(1, (acc, pos) => acc + GetBasinSum(points, pos, rowLength, visited));
    }

    private static List<int> GetAdjacentIndexes(IList<int> nodes, int pos, int rowLength)
    {
        var totalLength = nodes.Count;
        if (pos == 0)
        {
            return new List<int>() { pos + 1, pos + rowLength };
        }
        // Top right
        else if (pos == rowLength - 1)
        {
            return new List<int>() { pos + -1, pos + rowLength };
        }
        // Bottom left corner
        else if (pos == totalLength - rowLength)
        {
            return new List<int>() { pos + 1, pos - rowLength };
        }
        // Bottom right corner
        else if (pos == totalLength - 1)
        {
            return new List<int>() { pos - 1, pos - rowLength };
        }
        // Start of row
        else if (pos % rowLength == 0)
        {
            return new List<int>() { pos + 1, pos - rowLength, pos + rowLength, pos - rowLength };
        }
        // End of row
        else if (pos % rowLength == rowLength - 1)
        {
            return new List<int>() { pos - 1, pos - rowLength, pos + rowLength };
        }
        else
        {
            return new List<int>() { pos + 1, pos - 1, pos - rowLength, pos + rowLength }
            .Where(index => index >= 0 && index <= totalLength - 1).ToList();
        }
    }
}