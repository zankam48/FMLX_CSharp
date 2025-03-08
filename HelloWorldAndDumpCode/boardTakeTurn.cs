using System;
using System.Collections.Generic;

public class LudoBoard
{
    private const int BOARD_SIZE = 15;
    private string[][] board;

    private List<(int, int)> fullPaths;
    private Dictionary<string, List<(int, int)>> mainPaths;
    private Dictionary<string, List<(int, int)>> goalPaths;

    // Track positions for two players
    private int redPieceIndex = 0, greenPieceIndex = 0;
    private bool redFinished = false, greenFinished = false;

    // Turn management
    private bool isRedTurn = true;  // true for Red, false for Green

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

        fullPaths = new List<(int, int)>();
        mainPaths = new Dictionary<string, List<(int, int)>>();
        goalPaths = new Dictionary<string, List<(int, int)>>();

        InitializeFullPath();
        InitializeMainPaths();
        InitializeGoalPaths();
    }

    private void InitializeFullPath()
    {
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

    private void InitializeMainPaths()
    {
        mainPaths["Red"] = new List<(int, int)>(fullPaths);
        mainPaths["Green"] = GetWrappedPath(13);
    }

    private void InitializeGoalPaths()
    {
        goalPaths["Red"] = new List<(int, int)> { (7,5), (7,4), (7,3) };
        goalPaths["Green"] = new List<(int, int)> { (8,8), (7,8), (6,8) };
    }

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

    public void MovePiece(int steps)
    {
        string currentPlayer = isRedTurn ? "Red" : "Green";
        int currentPieceIndex = isRedTurn ? redPieceIndex : greenPieceIndex;
        bool currentFinished = isRedTurn ? redFinished : greenFinished;

        if (currentFinished)
        {
            Console.WriteLine($"{currentPlayer} piece has already finished!");
            return;
        }

        var path = mainPaths[currentPlayer];
        var goalPath = goalPaths[currentPlayer];

        int newIndex = currentPieceIndex + steps;

        if (newIndex < path.Count)
        {
            currentPieceIndex = newIndex;
        }
        else
        {
            int over = newIndex - path.Count;
            if (over < goalPath.Count)
            {
                currentPieceIndex = path.Count + over;
            }
            else
            {
                Console.WriteLine($"{currentPlayer} piece has FINISHED!");
                currentFinished = true;
            }
        }

        if (isRedTurn)
        {
            redPieceIndex = currentPieceIndex;
            redFinished = currentFinished;
        }
        else
        {
            greenPieceIndex = currentPieceIndex;
            greenFinished = currentFinished;
        }
    }

    public bool IsGameFinished()
    {
        return redFinished && greenFinished;
    }

    public void PrintBoard()
    {
        Console.WriteLine("\nCurrent Board:");
        Console.WriteLine($"Red Position: {redPieceIndex}, Green Position: {greenPieceIndex}");
    }

    public void SwitchTurn()
    {
        isRedTurn = !isRedTurn;
    }

    public string GetCurrentPlayer()
    {
        return isRedTurn ? "Red" : "Green";
    }
}

public class Program
{
    public static void Main()
    {
        var ludoBoard = new LudoBoard();
        Console.WriteLine("Two-player Ludo game: Red vs Green!");

        while (!ludoBoard.IsGameFinished())
        {
            string currentPlayer = ludoBoard.GetCurrentPlayer();
            Console.Write($"\n{currentPlayer}'s turn. Enter dice value (1-6) or 0 to quit: ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("Game ended.");
                break;
            }

            if (int.TryParse(input, out int steps) && steps >= 1 && steps <= 6)
            {
                ludoBoard.MovePiece(steps);
                ludoBoard.PrintBoard();
                ludoBoard.SwitchTurn();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            }
        }

        Console.WriteLine("Game over!");
    }
}
