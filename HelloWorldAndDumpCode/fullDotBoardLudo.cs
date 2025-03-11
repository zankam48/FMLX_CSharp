using System;

public class LudoBoard
{
    private const int BOARD_SIZE = 15;
    private string[][] board;

    /// <summary>
    /// Constructor: Initializes a 15×15 board with spaces,
    /// then places the 6×6 homes in each corner (R, B, G, Y).
    /// </summary>
    public LudoBoard()
    {
        // Create the 2D (jagged) array of strings
        board = new string[BOARD_SIZE][];
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            board[r] = new string[BOARD_SIZE];
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                board[r][c] = " ";
            }
        }
    }

    public void InitializeHomes()
    {
        // Red home
        MarkHome(0, 0, 6, 6, "R");

        // Blue home
        MarkHome(0, 9, 6, 15, "B");

        // Green home
        MarkHome(9, 0, 15, 6, "G");

        // Yellow home
        MarkHome(9, 9, 15, 15, "Y");
    }

    private void MarkHome(int startRow, int startCol, int endRow, int endCol, string symbol)
    {
        for (int r = startRow; r < endRow; r++)
        {
            for (int c = startCol; c < endCol; c++)
            {
                board[r][c] = symbol;
            }
        }
    }

    /// <summary>
    /// SetPath: uses MarkPath to create the main path and paths to goals.
    /// </summary>
    public void SetPath()
    {
        // Main path around the board
        MarkPath(7, 6, 7, 1);  // LEFT
        MarkPath(7, 1, 9, 1);  // DOWN
        MarkPath(9, 1, 9, 6);  // RIGHT
        MarkPath(10, 7, 15, 7); // DOWN
        MarkPath(15, 7, 15, 9); // RIGHT
        MarkPath(15, 9, 10, 9); // UP
        MarkPath(9, 10, 9, 15); // RIGHT       
        MarkPath(9, 15, 7, 15); // UP      
        MarkPath(7, 15, 7, 10); // LEFT
        MarkPath(6, 9, 1, 9);  // UP
        MarkPath(1, 9, 1, 7);  // LEFT
        MarkPath(1, 7, 6, 7);  // DOWN

        // Color-specific paths to goals
        RedPathToGoal();
        BluePathToGoal();
        GreenPathToGoal();
        YellowPathToGoal();
    }

    /// <summary>
    /// Red path to goal.
    /// </summary>
    private void RedPathToGoal()
    {
        // Red first path and rotation
        MarkPath(7, 2, 7, 1);  // LEFT
        MarkPath(7, 1, 8, 1);  // DOWN
        // Red path to goal
        MarkPath(8, 1, 8, 7);  // UP to goal
    }

    /// <summary>
    /// Blue path to goal.
    /// </summary>
    private void BluePathToGoal()
    {
        // Blue first path and rotation
        MarkPath(2, 9, 1, 9);  // UP
        MarkPath(1, 9, 1, 8);  // LEFT
        // Blue path to goal
        MarkPath(1, 8, 7, 8);  // DOWN to goal
    }

    /// <summary>
    /// Green path to goal.
    /// </summary>
    private void GreenPathToGoal()
    {
        // Green first path and rotation
        MarkPath(14, 7, 15, 7); // DOWN
        MarkPath(15, 7, 15, 8); // RIGHT
        // Green path to goal
        MarkPath(15, 8, 9, 8);  // UP to goal
    }

    /// <summary>
    /// Yellow path to goal.
    /// </summary>
    private void YellowPathToGoal()
    {
        // Yellow first path and rotation
        MarkPath(9, 14, 9, 15); // RIGHT
        MarkPath(9, 15, 8, 15); // UP
        // Yellow path to goal
        MarkPath(8, 15, 8, 9);  // LEFT to goal
    }

    /// <summary>
    /// Prints the current board to the console.
    /// </summary>
    public void PrintBoard()
    {
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            Console.WriteLine(string.Join(" ", board[r]));
        }
    }

    /// <summary>
    /// Marks a line of "." from (row1,col1) to (row2,col2), 1-based coords.
    /// Leaves R/B/G/Y cells alone, replacing only " " with ".".
    /// </summary>
    private void MarkPath(int row1, int col1, int row2, int col2)
    {
        // Convert from 1-based to 0-based
        int r1 = row1 - 1;
        int c1 = col1 - 1;
        int r2 = row2 - 1;
        int c2 = col2 - 1;

        // Determine deltas
        int dr = (r2 > r1) ? 1 : (r2 < r1) ? -1 : 0;
        int dc = (c2 > c1) ? 1 : (c2 < c1) ? -1 : 0;

        // Number of steps
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
}

public class Program
{
    public static void Main()
    {
        // Create a LudoBoard object
        var ludoBoard = new LudoBoard();

        ludoBoard.InitializeHomes();
        // Mark the dotted path on the board
        ludoBoard.SetPath();

        // Print the final board
        ludoBoard.PrintBoard();
    }
}
