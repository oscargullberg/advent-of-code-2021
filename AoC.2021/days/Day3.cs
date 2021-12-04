public class Day3
{
    public static int Part1(IEnumerable<string> lines)
    =>
        lines
        .Select(line => line.ToCharArray().Select((c, i) => (c, i)))
        .Aggregate(new Dictionary<int, Dictionary<char, int>>(), (acc, charList) =>
        charList.Aggregate(acc, (acc, bitWithPos) =>
         {
             var (bit, pos) = bitWithPos;
             if (!acc.ContainsKey(pos))
             {
                 acc[pos] = new Dictionary<char, int>();
             }
             acc[pos][bit] = acc[pos].ContainsKey(bit) ? acc[pos][bit] + 1 : 1;
             return acc;
         }),
        res => res.Select(x => x.Value.OrderBy(v => v.Value).Select(v => v.Key))
        )
        .Aggregate((gamma: "", epsilon: ""),
        (acc, curr) => (acc.gamma + curr.ElementAt(1), acc.epsilon + curr.First()),
        res => BinaryToDecimal(res.gamma) * BinaryToDecimal(res.epsilon));

    public static int Part2(IEnumerable<string> lines)
    => ToDecimalRating(lines, RatingType.OXYGEN) * ToDecimalRating(lines, RatingType.SCRUBBER);

    private static int ToDecimalRating(IEnumerable<string> lines, RatingType ratingType)
    => lines.Aggregate((lines: lines, i: 0), (acc, curr) =>
     {
         if (acc.lines.Count() == 1)
         {
             return acc;
         }
         var bitCounts = acc.lines.Select(line => line.ElementAt(acc.i))
         .GroupBy(c => c)
         .Select(g => (bit: g.Key, count: g.Count()))
         .OrderBy(g => g.count);
         var bit = ratingType switch
         {
             RatingType.OXYGEN => bitCounts.First().count == bitCounts.Last().count ? '1' : bitCounts.Last().bit,
             RatingType.SCRUBBER => bitCounts.First().count == bitCounts.Last().count ? '0' : bitCounts.First().bit,
             _ => throw new ArgumentException("Invalid rating type")
         };
         return (acc.lines.Where(l => l.ElementAt(acc.i) == bit), acc.i + 1);

     }, res => BinaryToDecimal(res.lines.First()));

    enum RatingType
    {
        OXYGEN,
        SCRUBBER
    }
    private static int BinaryToDecimal(string binaryNumber) => Convert.ToInt32(binaryNumber, 2);
}

