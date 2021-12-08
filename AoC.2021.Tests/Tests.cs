using Xunit;

public class Tests
{
    [Fact]
    public async Task Day1_Part1()
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath("day1.txt"));
        var res = Day1.Part1(input);
        Assert.Equal(1754, res);
    }

    [Fact]
    public async Task Day1_Part2()
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath("day1.txt"));
        var res = Day1.Part2(input);
        Assert.Equal(1789, res);
    }

    [Fact]
    public async Task Day2_Part1()
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath("day2.txt"));
        var res = Day2.Part1(input);
        Assert.Equal(1924923, res);
    }

    [Fact]
    public async Task Day2_Part2()
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath("day2.txt"));
        var res = Day2.Part2(input);
        Assert.Equal(1982495697, res);
    }

    [Fact]
    public async Task Day3_Part1()
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath("day3.txt"));
        var res = Day3.Part1(input);
        Assert.Equal(3687446, res);
    }

    [Theory]
    [InlineData("day3_part_2_sample.txt", 230)]
    [InlineData("day3.txt", 4406844)]
    public async Task Day3_Part2(string fileName, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(fileName));
        var res = Day3.Part2(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day4_sample.txt", 4512)]
    [InlineData("day4.txt", 2496)]
    public async Task Day4_Part1(string fileName, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(fileName));
        var res = Day4.Part1(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day4_sample.txt", 1924)]
    [InlineData("day4.txt", 25925)]
    public async Task Day4_Part2(string fileName, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(fileName));
        var res = Day4.Part2(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day5_sample.txt", 5)]
    [InlineData("day5.txt", 6005)]
    public async Task Day5_Part1(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day5.Part1(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day5_sample.txt", 12)]
    [InlineData("day5.txt", 23864)]
    public async Task Day5_Part2(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day5.Part2(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day6_sample.txt", 5934)]
    [InlineData("day6.txt", 394994)]
    public async Task Day6_Part1(string filename, long expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day6.Part1(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day6_sample.txt", 26984457539)]
    [InlineData("day6.txt", 1765974267455)]
    public async Task Day6_Part2(string filename, long expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day6.Part2(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day7_sample.txt", 37)]
    [InlineData("day7.txt", 333755)]
    public async Task Day7_Part1(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day7.Part1(input.First());
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day7_sample.txt", 168)]
    [InlineData("day7.txt", 94017638)]
    public async Task Day7_Part2(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day7.Part2(input.First());
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day8_sample.txt", 26)]
    [InlineData("day8.txt", 390)]
    public async Task Day8_Part1(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day8.Part1(input);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("day8_sample.txt", 61229)]
    [InlineData("day8.txt", 1011785)]
    public async Task Day8_Part2(string filename, int expected)
    {
        var input = await File.ReadAllLinesAsync(GetInputFilePath(filename));
        var res = Day8.Part2(input);
        Assert.Equal(expected, res);
    }

    private static string GetInputFilePath(string name) => Path.Combine(Directory.GetCurrentDirectory().Split($"bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}")[0], "inputs", name);
}