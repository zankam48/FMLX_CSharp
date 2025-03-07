public class LudoBoard
{
    private const int BOARD_SIZE = 15;
    private string[][] board;
    private List<Position> safeZones;
    private Dictionary<PieceColor, List<Position>> startPositions = new Dictionary<PieceColor, List<Position>>();
    private Dictionary<PieceColor, List<Position>> goalPositions = new Dictionary<PieceColor, List<Position>>();

    public LudoBoard()
    {
        board = new string[BOARD_SIZE][];
        safeZones = new List<Position>();

        for (int r = 0; r < BOARD_SIZE; r++)
        {
            board[r] = new string[BOARD_SIZE];
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                board[r][c] = " ";
            }
        }

        // Red home
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 6; c++)
            {
                board[r][c] = "R";
            }
        }

        // Blue home
        for (int r = 0; r < 6; r++)
        {
            for (int c = 9; c < 15; c++)
            {
                board[r][c] = "B";
            }
        }

        // Green home
        for (int r = 9; r < 15; r++)
        {
            for (int c = 0; c < 6; c++)
            {
                board[r][c] = "G";
            }
        }

        // Yellow home
        for (int r = 9; r < 15; r++)
        {
            for (int c = 9; c < 15; c++)
            {
                board[r][c] = "Y";
            }
        }
    }

    public void SetPath()
    {
        MarkPath(7, 6, 7, 1);
        MarkPath(7, 1, 9, 1);
        MarkPath(9, 1, 9, 6);
        MarkPath(10, 7, 15, 7);
        MarkPath(15, 7, 15, 9);
        MarkPath(15, 9, 10, 9); 
        MarkPath(9, 10, 9, 15);        
        MarkPath(9, 15, 7, 15);       
        MarkPath(7, 15, 7, 10);
        MarkPath(6, 9, 1, 9);
        MarkPath(1, 9, 1, 7);
        MarkPath(1, 7, 6, 7);
    }
    // row = y, col = x
    private void MarkPath(int row1, int col1, int row2, int col2)
    {
        int r1 = row1 - 1;
        int c1 = col1 - 1;
        int r2 = row2 - 1;
        int c2 = col2 - 1;

        int dr = (r2 > r1) ? 1 : (r2 < r1) ? -1 : 0;
        int dc = (c2 > c1) ? 1 : (c2 < c1) ? -1 : 0;

        int steps = Math.Max(Math.Abs(r2 - r1), Math.Abs(c2 - c1));

        int rr = r1;
        int cc = c1;

        for (int i = 0; i <= steps; i++)
        {
            if (board[rr][cc] == " ")
            {
                board[rr][cc] = ".";
            }
            rr += dr;
            cc += dc;
        }
    }

    public void PrintBoard()
    {
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            Console.WriteLine(string.Join(" ", board[r]));
        }
    }

    public bool IsOverlapped(Player player1, Player player2)
    {
        foreach (var piece1 in player1.GetPieces())
        {
            foreach (var piece2 in player2.GetPieces())
            {
                if (piece1.Position.Equals(piece2.Position) && !IsSafeZone(piece1.Position))
                {
                    if (!IsSafeZone(piece1.Position))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void DefineSafeZones()
    {   
        safeZones.Add(new Position(1,1));
        safeZones.Add(new Position(2,2));
        safeZones.Add(new Position(3,3));
        safeZones.Add(new Position(4,4));
    }

    public bool IsSafeZone(Position position)
    {
        return safeZones.Contains(position);
    }

    public bool IsAtHome(Piece piece)
    {
        return startPositions[piece.Color].Contains(piece.Position);
    }

    public bool IsAtGoal(Piece piece)
    {
        return goalPositions[piece.Color].Contains(piece.Position);
    }
    
}

