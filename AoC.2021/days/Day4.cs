public class Day4
{

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


    public static int Part1(IEnumerable<string> lines)
    {
        List<int> drawNumbers = GetDrawNumbers(lines);
        var boards = CreateBoards(lines);

        foreach (var number in drawNumbers)
        {
            foreach (var board in boards.Where(b => b.HasNumber(number)))
            {
                board.Check(number);
                if (board.IsBingo())
                {
                    return board.UncheckedSum() * number;
                }
            }
        }

        return -1;
    }

    public static int Part2(IEnumerable<string> lines)
    {
        List<int> drawNumbers = GetDrawNumbers(lines);
        var boards = CreateBoards(lines);

        var bingoBoards = new List<(int bingoNumber, Board board)>();
        foreach (var number in drawNumbers)
        {
            foreach (var board in boards.Where(b => b.HasNumber(number) && !bingoBoards.Any(bb => bb.board.Id == b.Id)))
            {
                board.Check(number);
                if (board.IsBingo())
                {
                    bingoBoards.Add((number, board));
                }
            }
        }

        var lastToBingo = bingoBoards.Last();
        return lastToBingo.bingoNumber * lastToBingo.board.UncheckedSum();
    }

    private static List<int> GetDrawNumbers(IEnumerable<string> lines) => lines.ElementAt(0).Split(',').Select(int.Parse).ToList();

    class Board
    {
        private const int ROW_COLUMN_SIZE = 5;
        private int[] _board;
        private int?[] _checked = new int?[25];

        public Board(int[] board)
        {
            _board = board;
        }

        public string Id => string.Join("-", _board);
        public bool HasNumber(int number) => _board.Contains(number);
        public bool HasCheckedPosition(int position) => _checked[position] != null;
        public bool HasCheckedNumber(int number) => _checked.Contains(number);
        public void Check(int number) => _checked[Array.IndexOf(_board, number)] = number;
        public int UncheckedSum() => _board.Except(_checked.Where(c => c.HasValue).Select(c => c.Value)).Sum();

        public bool IsBingo()
        {
            if (_checked.Count() < ROW_COLUMN_SIZE)
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