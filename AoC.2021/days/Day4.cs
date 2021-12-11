public class Day4
{
    public static int Part1(IEnumerable<string> lines)
    {
        List<int> drawNumbers = GetDrawNumbers(lines);
        var boards = CreateBoards(lines);

        return drawNumbers
        .Select(n => (num: n, boards: boards.Where(b => b.HasNumber(n))))
        .Aggregate(null as int?, (acc, curr) =>
        {
            if (acc.HasValue)
            {
                return acc;
            }

            var (num, boards) = curr;
            return boards.Select(b =>
            {
                b.Check(num);
                return b;
            })
           .Where(b => b.IsBingo())
           .Select(b => (b.UncheckedSum() * num) as int?)
           .FirstOrDefault();
        }) ?? -1;
    }

    public static int Part2(IEnumerable<string> lines)
    {
        List<int> drawNumbers = GetDrawNumbers(lines);
        var boards = CreateBoards(lines);

        var (board, num) = drawNumbers
        .Select(n => (num: n, boards: boards.Where(b => b.HasNumber(n)).ToList()))
        .Aggregate(new List<(Board board, int num)>(), (acc, curr) =>
        {
            var (num, boards) = curr;
            return acc.Concat(
                boards.Where(b => !acc.Select(a => a.board.Id).Contains(b.Id))
                .Select(b =>
                 {
                     b.Check(num);
                     return b;
                 })
                 .Where(b => b.IsBingo())
                 .Select(b => (board: b, num))
           ).ToList();
        }, res => res.Last());

        return num * board.UncheckedSum();
    }

    private static List<int> GetDrawNumbers(IEnumerable<string> lines) => lines.ElementAt(0).Split(',').Select(int.Parse).ToList();

    private static List<Board> CreateBoards(IEnumerable<string> lines) =>
    lines.Skip(1).Where(c => c != string.Empty)
    .Chunk(5)
    .Aggregate(Enumerable.Empty<int[]>(), (acc, curr) =>
    {
        var flatBoard = curr.SelectMany(row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse))
        .ToArray();
        return acc.Append(flatBoard);
    })
    .Select(b => new Board(b))
    .ToList();

    class Board
    {
        private const int ROW_COLUMN_SIZE = 5;
        private readonly int[] _board;
        private readonly int?[] _checked = new int?[25];

        public Board(int[] board)
        {
            _board = board;
        }

        public string Id => string.Join("-", _board);
        public bool HasNumber(int number) => _board.Contains(number);
        public bool HasCheckedPosition(int position) => _checked[position] != null;
        public bool HasCheckedNumber(int number) => _checked.Contains(number);
        public void Check(int number) => _checked[Array.IndexOf(_board, number)] = number;
        public int UncheckedSum() => _board.Except(_checked.Where(c => c.HasValue).Select(c => c!.Value)).Sum();

        public bool IsBingo()
        {
            if (_checked.Length < ROW_COLUMN_SIZE)
            {
                return false;
            }

            var isRowBingo = _board.Chunk(ROW_COLUMN_SIZE)
            .Any(row => row.All(HasCheckedNumber));
            if (isRowBingo)
            {
                return true;
            }

            for (var i = 0; i < ROW_COLUMN_SIZE - 1; i++)
            {
                var columnCheckCount = 0;
                foreach (var multiple in Enumerable.Range(0, ROW_COLUMN_SIZE))
                {
                    var pos = (ROW_COLUMN_SIZE * multiple) + i;
                    if (HasCheckedPosition(pos))
                    {
                        columnCheckCount++;
                    }
                    if (columnCheckCount == ROW_COLUMN_SIZE)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
    }
}