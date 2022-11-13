public class Day15
{
    public static int Part1(IEnumerable<string> lines)
    {
        var linesList = lines.ToList();
        var width = linesList.First().Length;
        var grid = linesList.SelectMany(line => line.Select(ch => int.Parse(ch.ToString()))).ToArray();

        return DjikstraSolve(grid, width, grid.Length - 1);
    }

    public static int Part2(IEnumerable<string> lines)
    {
        var linesList = lines.ToList();
        var gridTile = linesList.SelectMany(line => line.Select(ch => int.Parse(ch.ToString()))).ToArray();
        var tileWidth = lines.First().Length;

        var grid = CreateExpandedGrid(gridTile, tileWidth);
        return DjikstraSolve(grid, tileWidth * 5, grid.Length - 1);
    }

    private static int[] CreateExpandedGrid(int[] gridTile, int tileWidth)
    {
        var width = tileWidth * 5;
        var grid = new int[gridTile.Length * 25];
        var i = 0;
        var tileRow = 0;
        for (var row = 0; row < width; row++)
        {
            tileRow = row % tileWidth == 0 ? 0 : tileRow + 1;
            for (var tilePart = 0; tilePart < 5; tilePart++)
            {
                for (var rowI = 0; rowI < tileWidth; rowI++)
                {
                    grid[i] = GetNewTileValue();
                    i++;

                    int GetNewTileValue()
                    {
                        var originIndex = rowI + (tileRow * tileWidth);
                        var diff = (row / tileWidth) + tilePart;
                        return Enumerable.Range(0, diff).Aggregate(gridTile[originIndex], (acc, _) => (acc % 9) + 1);
                    }
                }
            }
        }
        return grid;
    }

    private static int DjikstraSolve(int[] grid, int width, int targetIndex)
    {
        var GetNodes = (int pos) => GetAdjacentNodes(pos, grid, width);
        var costs = new Dictionary<int, int>()
        {
            [targetIndex] = int.MaxValue
        };
        var processed = new HashSet<int>();
        var queue = new PriorityQueue<(int, int), int>();
        queue.Enqueue((0, 0), 0);
        while (queue.Count > 0)
        {
            var (index, cost) = queue.Dequeue();
            if (processed.Contains(index))
            {
                continue;
            }

            if (!costs.TryGetValue(index, out var currCost) || currCost > cost)
            {
                costs[index] = cost;
            }
            var nodes = GetNodes(index);
            foreach (var node in nodes)
            {
                var newCost = node.Cost + cost;
                queue.Enqueue((node.Index, newCost), newCost);
            }
            processed.Add(index);
        }
        return costs[targetIndex];
    }

    private static List<(int Index, int Cost)> GetAdjacentNodes(int index, int[] grid, int rowLength)
    {
        var beginningOfRow = index % rowLength == 0;
        var endOfRow = index % rowLength == rowLength - 1;
        var beginningOfLastRowPos = grid.Length - rowLength;

        var res = new List<(int, int)>();
        // Left
        if (!beginningOfRow)
        {
            var left = index - 1;
            res.Add((left, grid[left]));
        }
        // Rignt
        if (!endOfRow)
        {
            var right = index + 1;
            res.Add((right, grid[right]));
        }
        // Below
        if (index < beginningOfLastRowPos)
        {
            var below = index + rowLength;
            res.Add((below, grid[below]));
        }
        // Above
        if (index > rowLength - 1)
        {
            var above = index - rowLength;
            res.Add((above, grid[above]));
        }
        return res;
    }
}