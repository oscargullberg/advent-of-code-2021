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

    private static string GetInputFilePath(string name) => Path.Combine(Directory.GetCurrentDirectory().Split($"bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}")[0], "inputs", name);
}