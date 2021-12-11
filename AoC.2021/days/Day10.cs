public class Day10
{
    public static long Part1(IEnumerable<string> lines)
    {
        var openiningChars = new List<char> { '(', '{', '[', '<' };
        var closedToOpen = openiningChars.Zip(new List<char> { ')', '}', ']', '>' }, (o, c) => new { Open = o, Close = c }).ToDictionary(x => x.Close, x => x.Open);
        var closedToScore = new Dictionary<char, int> { { ')', 3 }, { '}', 1197 }, { ']', 57 }, { '>', 25137 } };

        var illegalChars = lines
        .Select(line => line.ToCharArray())
        .Aggregate(new List<char>(), (acc, chars) =>
        {
            var open = new Stack<char>();
            foreach (var ch in chars)
            {
                if (openiningChars.Contains(ch))
                {
                    open.Push(ch);
                }
                else
                {
                    var matchingOpen = closedToOpen[ch];
                    if (open.Peek() != matchingOpen)
                    {
                        acc.Add(ch);
                        break;
                    }
                    else
                    {
                        open.Pop();
                    }
                }
            }
            return acc;
        });

        return illegalChars
        .Select(x => closedToScore[x])
        .Sum();
    }

    public static long Part2(IEnumerable<string> lines)
    {
        var openiningChars = new List<char> { '(', '{', '[', '<' };
        var closedToOpen = openiningChars.Zip(new List<char> { ')', '}', ']', '>' }, (o, c) => new { Open = o, Close = c }).ToDictionary(x => x.Close, x => x.Open);
        var openToScore = new Dictionary<char, int> { { '(', 1 }, { '{', 3 }, { '[', 2 }, { '<', 4 } };

        var sortedScores = lines
        .Select(line => line.ToCharArray())
        .Select(GetOpen)
        .Where(open => open.Any())
        .Select(open => open.Aggregate(0L, (acc, ch) => acc * 5 + openToScore[ch]))
        .OrderBy(x => x).ToList();
        return sortedScores.ElementAt(sortedScores.Count / 2);

        Stack<char> GetOpen(char[] chars)
        {
            var open = new Stack<char>();
            foreach (var ch in chars)
            {
                if (openiningChars.Contains(ch))
                {
                    open.Push(ch);
                }
                else
                {
                    var matchingOpen = closedToOpen[ch];
                    // Corrupted, discard
                    if (open.Peek() != matchingOpen)
                    {
                        return new Stack<char>();
                    }
                    else
                    {
                        open.Pop();
                    }
                }
            }
            return open;
        }
    }

}