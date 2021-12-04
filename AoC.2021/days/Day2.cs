public class Day2
{
    public static int Part1(IEnumerable<string> lines)
    => lines.Select(Parse)
    .Aggregate((x: 0, y: 0), (acc, parsed) =>
     parsed.command switch
     {
         "forward" => (acc.x + parsed.units, acc.y),
         "down" => (acc.x, acc.y + parsed.units),
         "up" => (acc.x, acc.y - parsed.units),
         _ => acc
     }, (res) => res.x * res.y);


    public static int Part2(IEnumerable<string> lines)
    =>
    lines.Select(Parse)
    .Aggregate((x: 0, y: 0, aim: 0), (acc, curr) =>
    curr.command switch
     {
         "forward" => (acc.x + curr.units, acc.y + (acc.aim * curr.units), acc.aim),
         "down" => (acc.x, acc.y, acc.aim + curr.units),
         "up" => (acc.x, acc.y, acc.aim - curr.units),
         _ => acc
     }, res => res.x * res.y);

    private static (string command, int units) Parse(string line)
    {
        var parts = line.Split(' ');
        return (parts[0], int.Parse(parts[1]));
    }
}
