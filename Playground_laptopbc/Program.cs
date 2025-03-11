using System;
using System.Collections.Generic;

public class LudoBoard
{
    private const int BOARD_SIZE = 15;
    private string[][] board;

    // We'll store path data (row,col) in 0-based indexing.
    // For each color, we have a main path (full loop) and a goal path.
    private Dictionary<string, List<(int row, int col)>> mainPaths;
	private Dictionary<string, List<(int row, int col)>> goalPaths;
    private List<(int row, int col)> fullPaths;


    // Track piece's current position
    // If pieceIndex < mainPaths[color].Count => still on main path
    // else => we are in the goal path
    private int pieceIndex = 0;        // number of steps traveled so far
    private bool pieceFinished = false;

    // Selected color and its marker
    private string pieceColor = "";    // "Red"/"Blue"/"Green"/"Yellow"
    private string pieceMarker = "";   // "R"/"B"/"G"/"Y" for board display

    public LudoBoard()
    {
        // 1) Create the 2D (jagged) array of strings
        board = new string[BOARD_SIZE][];
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            board[r] = new string[BOARD_SIZE];
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                board[r][c] = " ";
            }
        }

        // 2) Initialize path dictionaries
        // For demonstration, we use short, fictitious loops (not real Ludo).
        // Adjust these to match your actual track layout.
        
        mainPaths = new Dictionary<string, List<(int, int)>>();
        goalPaths = new Dictionary<string, List<(int, int)>>();
        fullPaths = new List<(int, int)>();

        // We store a short main path (loop) for each color, then a small goal path
        // All coords are 0-based: (row,col).
        // *** SAMPLE PATHS -- change as you like for your board ***
        
        // RED goal path
        // goalPaths["Red"] = new List<(int, int)> {
        //     (7,5), (7,4), (7,3) // A small 3-step path to 'finish'
        // };

        // goalPaths["Blue"] = new List<(int, int)> {
        //     (1,8), (2,8), (3,8)
        // };

        // goalPaths["Green"] = new List<(int, int)> {
        //     (8,8), (7,8), (6,8)
        // };

        // goalPaths["Yellow"] = new List<(int, int)> {
        //     (8,5), (7,5), (6,5)
        // };
        InitializeFullPath();
        InitializeMainPaths();
        InitializeGoalPaths();
    }
    
    private void InitializeFullPath()
    {
        
        // Unified main path
        fullPaths.AddRange(new List<(int, int)> {
            (6,1), (6,0), (7,0), (8,0), (8,1), (8,2), (8,3), (8,4), (8,5),
            (9,6), (10,6), (11,6), (12,6), (13,6), (14,6), (14,7), 
            (14,8), (13,8), (12,8), (11,8), (10,8), (9,8), (8,9), (8,10), 
            (8,11), (8,12), (8,13), (8,14), (7,14), (6,14), (6,13), 
            (6,12), (6,11), (6,10), (6,9), (5,8), (4,8), (3,8), (2,8), 
            (1,8), (0,8), (0,7), (0,6), (1,6), (2,6), (3,6), 
            (4,6), (5,6), (6,5), (6,4), (6,3), (6,2)
        });
    }

    /// <summary>
    /// Initializes main paths for each color using slices of fullPath.
    /// </summary>
    private void InitializeMainPaths()
    {
        mainPaths["Red"] = new List<(int, int)>(fullPaths);
        mainPaths["Green"] = GetWrappedPath(13);
        mainPaths["Yellow"] = GetWrappedPath(26);
        mainPaths["Blue"] = GetWrappedPath(39);
    }

    /// <summary>
    /// Returns a wrapped path starting at a given index.
    /// </summary>
    private List<(int, int)> GetWrappedPath(int startIndex)
    {
        var wrappedPath = new List<(int, int)>();
        int pathLength = fullPaths.Count;
        
        for (int i = 0; i < pathLength; i++)
        {
            wrappedPath.Add(fullPaths[(startIndex + i) % pathLength]);
        }
        
        return wrappedPath;
    }
    // 7,2 : red first path, red has to rotate first meaning follow the markpath first before path to goal
    // 7,2 7,1 8,1 : if one of the red piece is at this coordinate 2x means it has completed a rotation and will proceed to go to path to the goal 
    // 8,1 -> 8,7 (goal) : path to goal

    // 14,7 -> green first path
    // 14,7 15,7 15,8 ->
    // 15,8 -> 9,8 (goal)

    // 9,14 -> yellow first path
    // 9,14 9,15 8,15
    // 8,15 -> 8,9 (goal)

    // 2,9
    // 2,9 1,9 1,8
    // 1,8 -> 7,8 (goal)
    private void InitializeGoalPaths()
    {
        goalPaths["Red"] = new List<(int, int)> { (6,1), (6,0), (7,0), (7,1), (7,2), (7,3), (7,4), (7,5), (7,6) };
        goalPaths["Green"] = new List<(int, int)> { (13,6), (14,6), (14,7), (13,7), (12,7), (11,7), (10,7), (9,7), (8,7) };
        goalPaths["Yellow"] = new List<(int, int)> { (8,13), (8,14), (7,14), (7,13), (7,12), (7,11), (7,10), (7,9), (7,8)};
        goalPaths["Blue"] = new List<(int, int)> { (1,8), (0,8), (0,7), (1,7), (2,7), (3,7), (4,7), (5,7), (6,7) };
    }

    /// <summary>
    /// Fills corner homes (like your code).
    /// </summary>
    public void InitializeHomes()
    {
        MarkHome(0, 0, 6, 6, " ");    // Red home
        MarkHome(0, 9, 6, 15, " ");   // Blue home
        MarkHome(9, 0, 15, 6, " ");   // Green home
        MarkHome(9, 9, 15, 15, " ");  // Yellow home

        AssignUnicodeChars();
    }

    private void AssignUnicodeChars()
    {
        // Red home positions
        board[1][1] = "Р";  // Cyrillic R
        board[1][4] = "Ⓡ";  // Circled R
        board[4][1] = "Ꞃ";  // Latin R with stroke
        board[4][4] = "ᴦ";  // Greek Gamma (looks like R)

        // Green home positions
        board[10][1] = "Ԍ";  // Cyrillic G
        board[13][1] = "Ḡ";  // G with macron
        board[10][4] = "Ɡ";   // Latin G with stroke
        board[13][4] = "𝒢";  // Script G

        // Blue home positions
        board[1][10] = "В";   // Cyrillic B
        board[1][13] = "Ḃ";  // B with dot
        board[4][10] = "𐌁";  // Old Italic B
        board[4][13] = "𝓑";  // Script B

        // Yellow home positions
        board[10][10] = "Ү";   // Cyrillic Y
        board[13][10] = "Ẏ";  // Y with dot
        board[10][13] = "Ƴ";  // Y with hook
        board[13][13] = "𝓨"; // Script Y
    }

    /// <summary>
    /// Mark the track with "." visually. (Optional)
    /// </summary>
    public void SetPath()
    {
        // Just a few lines to show some dots. 
        // You might replicate calls to MarkPath(...) from your original code
        // to match the actual board layout with ".".
        // For brevity, let's just mark the entire row 6 and 9 with "." as an example:
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

    public void PrintBoard()
    {
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            Console.WriteLine(string.Join(" ", board[r]));
        }
    }

    // =============== Movement Logic ===============

    /// <summary>
    /// Choose color and set piece marker. Then place piece at mainPaths[color][0].
    /// </summary>
    public void ChoosePieceColor()
    {
        // Prompt for color
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
        // Normalize to capitalized
        pieceColor = char.ToUpper(input[0]) + input.Substring(1).ToLower();

        // Set piece marker
        switch (pieceColor)
        {
            case "Red": pieceMarker = "\u001b[31mR\u001b[0m"; break;
            case "Blue": pieceMarker = "\u001b[34mB\u001b[0m"; break;
            case "Green": pieceMarker = "\u001b[32mG\u001b[0m"; break;
            case "Yellow": pieceMarker = "\u001b[33m\u001b[0m"; break;
        }

        pieceIndex = 0;         // start at index 0 (on main path)
        pieceFinished = false;

        // Place piece on the board
        var (row, col) = mainPaths[pieceColor][pieceIndex];
        board[row][col] = pieceMarker;
    }

    /// <summary>
    /// Move the piece by 'steps' along the main path, then into goal if needed.
    /// </summary>
    public void MovePiece(int steps)
    {
        if (pieceFinished)
        {
            Console.WriteLine("Piece has already finished!");
            return;
        }

        // 1) Remove from current board location -> restore "."
        //    (or space if you prefer, but likely "." for path)
        var (oldRow, oldCol) = GetCurrentCoordinates();
        if (board[oldRow][oldCol] == pieceMarker)
        {
            board[oldRow][oldCol] = "."; 
        }

        // 2) Calculate new position
        int newIndex = pieceIndex + steps;

        // 3) Check if newIndex extends into the goal path
        int mainCount = mainPaths[pieceColor].Count;   // how many squares in main path
        int goalCount = goalPaths[pieceColor].Count;   // how many squares in goal path

        if (newIndex < mainCount)
        {
            // Still on the main path
            pieceIndex = newIndex;
            var (r, c) = mainPaths[pieceColor][pieceIndex];
            board[r][c] = pieceMarker;
        }
        else
        {
            // Potentially in the goal path
            int over = newIndex - mainCount;  // how many steps into the goal path
            if (over < goalCount)
            {
                // In the goal path, place on goal path index = 'over'
                pieceIndex = newIndex;  // track total steps traveled
                var (gr, gc) = goalPaths[pieceColor][over];
                board[gr][gc] = pieceMarker;
            }
            else
            {
                // Beyond the final goal square => piece finished
                Console.WriteLine($"{pieceColor} piece has FINISHED!");
                pieceFinished = true;
                pieceIndex = mainCount + goalCount; // or newIndex
            }
        }
    }

    /// <summary>
    /// Get the 0-based (row,col) where the piece currently stands.
    /// If pieceIndex < mainPath.Count => main path
    /// else => goal path
    /// </summary>
    private (int row, int col) GetCurrentCoordinates()
    {
        int mainCount = mainPaths[pieceColor].Count;
        if (pieceIndex < mainCount)
        {
            return mainPaths[pieceColor][pieceIndex];
        }
        else
        {
            // in goal path
            int over = pieceIndex - mainCount;
            if (over < goalPaths[pieceColor].Count)
            {
                return goalPaths[pieceColor][over];
            }
            else
            {
                // Off the board (finished), pick last goal square
                over = goalPaths[pieceColor].Count - 1;
                return goalPaths[pieceColor][Math.Max(over,0)];
            }
        }
    }

    // =============== Helper ===============

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

        // 1) Place R/B/G/Y homes
        ludoBoard.InitializeHomes();

        // 2) Mark some dots for visual reference
        ludoBoard.SetPath();

        // 3) Ask user which color to use, place the piece
        ludoBoard.ChoosePieceColor();

        // 4) Show initial board
        PrintAndPause(ludoBoard);

        // 5) Repeatedly prompt for dice input
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

            // Move piece
            ludoBoard.MovePiece(steps);

            // Show board
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