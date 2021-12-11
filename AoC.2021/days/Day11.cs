public class Day11
{
    public static long Part1(IEnumerable<string> lines)
    {
        var rowLength = lines.First().Length;
        var grid = lines.SelectMany(line => line.ToCharArray())
        .Select(ch => int.Parse(ch.ToString()))
        .ToArray();

        var flashCount = 0;
        for (var step = 0; step < 100; step++)
        {
            var flashed = ProcessStep(grid, rowLength);
            flashCount += flashed.Count;
        }
        return flashCount;
    }

    public static long Part2(IEnumerable<string> lines)
    {
        var rowLength = lines.First().Length;
        var grid = lines.SelectMany(line => line.ToCharArray())
        .Select(ch => int.Parse(ch.ToString()))
        .ToArray();

        var step = 1;
        while (true)
        {
            var flashed = ProcessStep(grid, rowLength);
            if (flashed.Count == grid.Length)
            {
                return step;
            }
            step++;
        }
    }

    private static HashSet<int> ProcessStep(int[] grid, int rowLength)
    {
        var flashed = new HashSet<int>();
        for (var i = 0; i < grid.Length; i++)
        {
            grid[i] += 1;
            if (grid[i] > 9)
            {
                Flash(i, grid, rowLength, flashed);
            }
        }

        foreach (var pos in flashed)
        {
            grid[pos] = 0;
        }
        return flashed;
    }

    private static void Flash(int pos, int[] grid, int rowLength, HashSet<int> flashed)
    {
        if (flashed.Contains(pos))
        {
            return;
        }

        flashed.Add(pos);
        foreach (var i in GetAdjacentIndexes(grid, pos, rowLength))
        {
            grid[i] += 1;
            if (grid[i] > 9)
            {
                Flash(i, grid, rowLength, flashed);
            }
        }
    }

    private static List<int> GetAdjacentIndexes(int[] grid, int pos, int rowLength)
    {
        var totalLength = grid.Length;
        if (pos == 0)
        {
            return new List<int>() { pos + 1, pos + rowLength, pos + rowLength + 1 };
        }
        // Top right
        else if (pos == rowLength - 1)
        {
            return new List<int>() { pos - 1, pos + rowLength, pos + rowLength - 1 };
        }
        // Bottom left corner
        else if (pos == totalLength - rowLength)
        {
            return new List<int>() { pos + 1, pos - rowLength, pos - rowLength + 1 };
        }
        // Bottom right corner
        else if (pos == totalLength - 1)
        {
            return new List<int>() { pos - 1, pos - rowLength, pos - rowLength - 1 };
        }
        // Start of row
        else if (pos % rowLength == 0)
        {
            return new List<int>() { pos + 1, pos - rowLength, pos - rowLength + 1, pos + rowLength, pos + rowLength + 1 };
        }
        // End of row
        else if (pos % rowLength == rowLength - 1)
        {
            return new List<int>() { pos - 1, pos - rowLength, pos - rowLength - 1, pos + rowLength, pos + rowLength - 1 };
        }
        else
        {
            return new List<int>() { pos + 1, pos - 1, pos - rowLength - 1, pos - rowLength, pos - rowLength + 1, pos + rowLength, pos + rowLength - 1, pos + rowLength + 1 }
            .Where(index => index >= 0 && index <= totalLength - 1).ToList();
        }
    }
}