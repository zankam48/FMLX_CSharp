using System;
using System.Collections.Generic;

public class LudoBoard
{
    private const int BOARD_SIZE = 15;
    private string[][] board;

    private Dictionary<string, List<(int row, int col)>> mainPaths;
	private Dictionary<string, List<(int row, int col)>> goalPaths;

    private int pieceIndex = 0;       
    private bool pieceFinished = false;

    private string pieceColor = "";    
    private string pieceMarker = "";  

    public LudoBoard()
    {
        board = new string[BOARD_SIZE][];
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            board[r] = new string[BOARD_SIZE];
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                board[r][c] = " ";
            }
        }

        mainPaths = new Dictionary<string, List<(int, int)>>();
        goalPaths = new Dictionary<string, List<(int, int)>>();

        
        // mainPaths = new List<int, int> {
        //     // red from (6,1) idx 0
        //     // green idx 13
        //     // yellow idx 26
        //     // blue idx 39
        //     (6,1), (6,0), (7,0), (8,0), (8,1), (8,2), (8,3), (8,4), (8,5),
        //     (9,6), (10,6), (11,6), (12,6), (13,6), (14,6), (14,7), 
        //     (14,8), (13,8), (12,8), (11,8), (10,8), (9,8), (8,9), (8,10), 
        //     (8,11), (8,12), (8,13), (8,14), (7,14), (6,14), (6,13), 
        //     (6,12), (6,11), (6,10), (6,9), (5,8), (4,8), (3,8), (2,8), 
        //     (1,8), (0,8), (0,7), (0,6), (1,6), (2,6), (3,6), 
        //     (4,6), (5,6), (6,5), (6,4), (6,3), (6,2),
        // };
        // RED main path
        mainPaths["Red"] = new List<(int, int)> {
            (6,5), (6,4), (6,3), (6,2), (6,1), (6,0), (7,0), (8,0), (8,1), (8,2), (8,3), (8,4), (8,5),
            (9,6), (10,6), (11,6), (12,6), (13,6), (14,6), (14,7), 
            (14,8), (13,8), (12,8), (11,8), (10,8), (9,8), (8,9), (8,10), 
            (8,11), (8,12), (8,13), (8,14), (7,14), (6,14), (6,13), 
            (6,12), (6,11), (6,10), (6,9), (5,8), (4,8), (3,8), (2,8), 
            (1,8), (0,8), (0,7), (0,6), (1,6), (2,6), (3,6), 
            (4,6), (5,6),
        };
        // RED goal path
        goalPaths["Red"] = new List<(int, int)> {
            (7,5), (7,4), (7,3) // A small 3-step path to 'finish'
        };

        // BLUE main path
        mainPaths["Blue"] = new List<(int, int)> {
            (0,9), (0,8), (0,7), (0,6), (0,5),
            (1,5), (2,5), (3,5), (4,5), (5,5),
            (5,6), (5,7), (5,8), (5,9),
            (4,9), (3,9), (2,9), (1,9)
        };
        // BLUE goal path
        goalPaths["Blue"] = new List<(int, int)> {
            (1,8), (2,8), (3,8)
        };

        // GREEN main path
        mainPaths["Green"] = new List<(int, int)> {
            (9,9), (8,9), (7,9), (6,9), (5,9),
            (5,8), (5,7), (5,6), (5,5),
            (6,5), (7,5), (8,5), (9,5),
            (9,6), (9,7), (9,8)
        };
        // GREEN goal path
        goalPaths["Green"] = new List<(int, int)> {
            (8,8), (7,8), (6,8)
        };

        // YELLOW main path
        mainPaths["Yellow"] = new List<(int, int)> {
            (9,6), (9,5), (9,4), (9,3), (9,2),
            (8,2), (7,2), (6,2), (5,2),
            (5,3), (5,4), (5,5), (5,6),
            (6,6), (7,6), (8,6), (9,6)
        };
        // YELLOW goal path
        goalPaths["Yellow"] = new List<(int, int)> {
            (8,5), (7,5), (6,5)
        };
    }

    public void InitializeHomes()
    {
        MarkHome(0, 0, 6, 6, "R");    // Red home
        MarkHome(0, 9, 6, 15, "B");   // Blue home
        MarkHome(9, 0, 15, 6, "G");   // Green home
        MarkHome(9, 9, 15, 15, "Y");  // Yellow home
    }

    public void SetPath()
    {
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
        MarkPath(1, 7, 6, 7);
    }

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

    public void ChoosePieceColor()
    {
        Console.Write("Choose your piece color (Red/Blue/Green/Yellow): ");
        string input = Console.ReadLine().Trim();
        while (!(input.Equals("Red", StringComparison.OrdinalIgnoreCase)
              || input.Equals("Blue", StringComparison.OrdinalIgnoreCase)
              || input.Equals("Green", StringComparison.OrdinalIgnoreCase)
              || input.Equals("Yellow", StringComparison.OrdinalIgnoreCase)))
        {
            Console.Write("Invalid choice. Please choose Red, Blue, Green, or Yellow: ");
            input = Console.ReadLine().Trim();
        }
        pieceColor = char.ToUpper(input[0]) + input.Substring(1).ToLower();

        switch (pieceColor)
        {
            case "Red": pieceMarker = "R"; break;
            case "Blue": pieceMarker = "B"; break;
            case "Green": pieceMarker = "G"; break;
            case "Yellow": pieceMarker = "Y"; break;
        }

        pieceIndex = 0;       
        pieceFinished = false;

        var (row, col) = mainPaths[pieceColor][pieceIndex];
        board[row][col] = pieceMarker;
    }

    public void MovePiece(int steps)
    {
        if (pieceFinished)
        {
            Console.WriteLine("Piece has already finished!");
            return;
        }

        var (oldRow, oldCol) = GetCurrentCoordinates();
        if (board[oldRow][oldCol] == pieceMarker)
        {
            board[oldRow][oldCol] = "."; 
        }

        int newIndex = pieceIndex + steps;

        int mainCount = mainPaths[pieceColor].Count;   
        int goalCount = goalPaths[pieceColor].Count;   

        if (newIndex < mainCount)
        {
            pieceIndex = newIndex;
            var (r, c) = mainPaths[pieceColor][pieceIndex];
            board[r][c] = pieceMarker;
        }
        else
        {
            int over = newIndex - mainCount;  
            if (over < goalCount)
            {
                pieceIndex = newIndex;  
                var (gr, gc) = goalPaths[pieceColor][over];
                board[gr][gc] = pieceMarker;
            }
            else
            {
                Console.WriteLine($"{pieceColor} piece has FINISHED!");
                pieceFinished = true;
                pieceIndex = mainCount + goalCount; 
            }
        }
    }

    private (int row, int col) GetCurrentCoordinates()
    {
        int mainCount = mainPaths[pieceColor].Count;
        if (pieceIndex < mainCount)
        {
            return mainPaths[pieceColor][pieceIndex];
        }
        else
        {
            int over = pieceIndex - mainCount;
            if (over < goalPaths[pieceColor].Count)
            {
                return goalPaths[pieceColor][over];
            }
            else
            {
                over = goalPaths[pieceColor].Count - 1;
                return goalPaths[pieceColor][Math.Max(over,0)];
            }
        }
    }


    // red home -> (1,1) (1,4) (4,1) (4,4)
    // green home -> (10,1) (13,1) (10,4) (13,4)
    // blue home -> (1,10) (1,13) (4,10) (4,13) 
    // yellow home -> (10,10) (13,10) (10,13) (13,13) 
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
}

public class Program
{
    public static void Main()
    {
        var ludoBoard = new LudoBoard();

        ludoBoard.InitializeHomes();

        ludoBoard.SetPath();

        ludoBoard.ChoosePieceColor();

        PrintAndPause(ludoBoard);

        while (true)
        {
            Console.Write("\nEnter a dice value (1-6) to move, or 0 to quit: ");
            string input = Console.ReadLine().Trim();
            int steps;
            if (!int.TryParse(input, out steps))
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
                continue;
            }
            if (steps == 0)
            {
                Console.WriteLine("Exiting game.");
                break;
            }
            if (steps < 1 || steps > 6)
            {
                Console.WriteLine("Dice must be between 1 and 6.");
                continue;
            }

            ludoBoard.MovePiece(steps);

            PrintAndPause(ludoBoard);
        }

        Console.WriteLine("Game ended. Press Enter to exit.");
        Console.ReadLine();
    }

    private static void PrintAndPause(LudoBoard board)
    {
        board.PrintBoard();
        Console.WriteLine();
    }
}
