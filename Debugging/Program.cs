using System;
using System.Collections.Generic;

namespace LudoDemo
{
    public enum PieceColor { RED, BLUE, GREEN, YELLOW }

    // Each cell on the board.
    public class Square
    {
        public int Row { get; set; }
        public int Col { get; set; }
        // Current occupant (could be a piece marker or the underlying marker)
        public string Occupant { get; set; }
        // BaseMarker stores what this square should display when no piece is on it.
        public string BaseMarker { get; set; }

        public Square(int row, int col)
        {
            Row = row;
            Col = col;
            Occupant = " ";
            BaseMarker = " ";
        }
        public bool IsOccupied() => Occupant != " ";
    }

    // A path is an ordered list of squares.
    public class Path
    {
        private List<Square> squares;
        public Path() { squares = new List<Square>(); }
        public void AddSquare(Square square) { squares.Add(square); }
        public Square GetSquare(int index)
        {
            if (index >= 0 && index < squares.Count)
                return squares[index];
            return null;
        }
        public int Count => squares.Count;
        public List<Square> GetSquares() => squares;
    }

    // The PathManager holds the full path as well as each color's main and goal paths.
    public class PathManager
    {
        private Board board;
        private Path fullPath;
        private Dictionary<PieceColor, Path> mainPaths;
        private Dictionary<PieceColor, Path> goalPaths;

        public PathManager(Board board)
        {
            this.board = board;
            fullPath = new Path();
            mainPaths = new Dictionary<PieceColor, Path>();
            goalPaths = new Dictionary<PieceColor, Path>();

            InitializeFullPath();
            InitializeMainPaths();
            InitializeGoalPaths();
        }

        private void InitializeFullPath()
        {
            // These coordinates (0-indexed) define a loop around the board.
            List<(int, int)> coordinates = new List<(int, int)>
            {
                (6,1),(6,0),(7,0),(8,0),(8,1),(8,2),(8,3),(8,4),(8,5),
                (9,6),(10,6),(11,6),(12,6),(13,6),(14,6),(14,7),(14,8),(13,8),(12,8),(11,8),
                (10,8),(9,8),(8,9),(8,10),(8,11),(8,12),(8,13),(8,14),(7,14),(6,14),(6,13),
                (6,12),(6,11),(6,10),(6,9),(5,8),(4,8),(3,8),(2,8),(1,8),(0,8),(0,7),(0,6),
                (1,6),(2,6),(3,6),(4,6),(5,6),(6,5),(6,4),(6,3),(6,2)
            };

            foreach (var (r, c) in coordinates)
            {
                Square sq = board.GetSquare(r, c);
                fullPath.AddSquare(sq);
            }
        }

        private void InitializeMainPaths()
        {
            // Create a wrapped main path for each color.
            mainPaths[PieceColor.RED] = GetWrappedPath(0);
            mainPaths[PieceColor.GREEN] = GetWrappedPath(13);
            mainPaths[PieceColor.YELLOW] = GetWrappedPath(26);
            mainPaths[PieceColor.BLUE] = GetWrappedPath(39);
        }
        private Path GetWrappedPath(int startIndex)
        {
            Path path = new Path();
            int total = fullPath.Count;
            for (int i = 0; i < total; i++)
            {
                int idx = (startIndex + i) % total;
                path.AddSquare(fullPath.GetSquare(idx));
            }
            return path;
        }

        private void InitializeGoalPaths()
        {
            // Red goal path
            Path redGoal = new Path();
            List<(int, int)> redGoalCoords = new List<(int, int)>
            {
                (6,1),(6,0),(7,0),(7,1),(7,2),(7,3),(7,4),(7,5),(7,6)
            };
            foreach (var (r, c) in redGoalCoords)
                redGoal.AddSquare(board.GetSquare(r, c));
            goalPaths[PieceColor.RED] = redGoal;

            // Green goal path
            Path greenGoal = new Path();
            List<(int, int)> greenGoalCoords = new List<(int, int)>
            {
                (13,6),(14,6),(14,7),(13,7),(12,7),(11,7),(10,7),(9,7),(8,7)
            };
            foreach (var (r, c) in greenGoalCoords)
                greenGoal.AddSquare(board.GetSquare(r, c));
            goalPaths[PieceColor.GREEN] = greenGoal;

            // Yellow goal path
            Path yellowGoal = new Path();
            List<(int, int)> yellowGoalCoords = new List<(int, int)>
            {
                (8,13),(8,14),(7,14),(7,13),(7,12),(7,11),(7,10),(7,9),(7,8)
            };
            foreach (var (r, c) in yellowGoalCoords)
                yellowGoal.AddSquare(board.GetSquare(r, c));
            goalPaths[PieceColor.YELLOW] = yellowGoal;

            // Blue goal path
            Path blueGoal = new Path();
            List<(int, int)> blueGoalCoords = new List<(int, int)>
            {
                (1,8),(0,8),(0,7),(1,7),(2,7),(3,7),(4,7),(5,7),(6,7)
            };
            foreach (var (r, c) in blueGoalCoords)
                blueGoal.AddSquare(board.GetSquare(r, c));
            goalPaths[PieceColor.BLUE] = blueGoal;
        }

        public Path GetFullPath() => fullPath;
        public Path GetMainPath(PieceColor color) => mainPaths[color];
        public Path GetGoalPath(PieceColor color) => goalPaths[color];
    }

    // The Board class manages the grid of squares and movement logic.
    public class Board
    {
        public const int BOARD_SIZE = 15;
        private Square[,] grid;
        public PathManager PathManager { get; private set; }

        public Board()
        {
            grid = new Square[BOARD_SIZE, BOARD_SIZE];
            for (int r = 0; r < BOARD_SIZE; r++)
            {
                for (int c = 0; c < BOARD_SIZE; c++)
                {
                    grid[r, c] = new Square(r, c);
                }
            }
            MarkSafeZones();
            PathManager = new PathManager(this);
            InitializePathVisuals();
        }

        public Square GetSquare(int row, int col)
        {
            if (row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE)
                return grid[row, col];
            return null;
        }

        // Mark safe zones and update both Occupant and BaseMarker to "*".
        private void MarkSafeZones()
        {
            // Safe zone coordinates (0-indexed)
            List<(int, int)> safeCoords = new List<(int, int)>
            {
                (13,6), (12,8), (8,13), (6,12), (1,8), (2,6), (6,1), (8,2)
            };
            foreach (var (r, c) in safeCoords)
            {
                Square sq = GetSquare(r, c);
                if (sq != null)
                {
                    sq.Occupant = "*";
                    sq.BaseMarker = "*";
                }
            }
        }

        // Mark path squares with a dot, but only if they are not safe zones.
        private void InitializePathVisuals()
        {
            foreach (Square sq in PathManager.GetFullPath().GetSquares())
            {
                if (sq.BaseMarker == " ") // not already marked (e.g., safe zone)
                {
                    sq.Occupant = ".";
                    sq.BaseMarker = ".";
                }
            }
        }

        // Updates a piece's position:
        //   - The old square's occupant is reset to its BaseMarker.
        //   - The new square is set to the piece's marker.
        public void UpdatePiecePosition(string pieceMarker, Square oldSquare, Square newSquare)
        {
            if (oldSquare != null)
            {
                // Restore the square's original marker.
                oldSquare.Occupant = oldSquare.BaseMarker;
            }
            if (newSquare != null)
                newSquare.Occupant = pieceMarker;
        }

        public void PrintBoard()
        {
            for (int r = 0; r < BOARD_SIZE; r++)
            {
                for (int c = 0; c < BOARD_SIZE; c++)
                {
                    Console.Write(grid[r, c].Occupant + " ");
                }
                Console.WriteLine();
            }
        }
    }

    // Main demonstration: choose a color and move the piece.
    public class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            Console.Write("Choose your piece color (Red/Blue/Green/Yellow): ");
            string input = Console.ReadLine().Trim().ToLower();
            PieceColor chosenColor;
            while (!Enum.TryParse(input, true, out chosenColor) ||
                   !Enum.IsDefined(typeof(PieceColor), chosenColor))
            {
                Console.Write("Invalid choice. Please choose Red, Blue, Green, or Yellow: ");
                input = Console.ReadLine().Trim().ToLower();
            }

            // Set the piece marker using ANSI escape codes.
            string pieceMarker = "";
            switch (chosenColor)
            {
                case PieceColor.RED: pieceMarker = "\u001b[31m1\u001b[0m"; break;
                case PieceColor.BLUE: pieceMarker = "\u001b[34mB\u001b[0m"; break;
                case PieceColor.GREEN: pieceMarker = "\u001b[32mG\u001b[0m"; break;
                case PieceColor.YELLOW: pieceMarker = "\u001b[33mY\u001b[0m"; break;
            }

            // Get the main and goal paths for the chosen color.
            Path mainPath = board.PathManager.GetMainPath(chosenColor);
            Path goalPath = board.PathManager.GetGoalPath(chosenColor);

            // Place the piece at the start of the main path.
            int pieceIndex = 0;
            Square currentSquare = mainPath.GetSquare(pieceIndex);
            currentSquare.Occupant = pieceMarker;

            board.PrintBoard();

            while (true)
            {
                Console.Write("\nEnter dice roll (1-6) to move or 0 to quit: ");
                if (!int.TryParse(Console.ReadLine(), out int dice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                if (dice == 0)
                {
                    Console.WriteLine("Exiting game.");
                    break;
                }
                if (dice < 1 || dice > 6)
                {
                    Console.WriteLine("Dice must be between 1 and 6.");
                    continue;
                }

                int newIndex = pieceIndex + dice;
                Square newSquare = null;

                if (newIndex < mainPath.Count)
                {
                    newSquare = mainPath.GetSquare(newIndex);
                }
                else
                {
                    int over = newIndex - mainPath.Count;
                    if (over < goalPath.Count)
                    {
                        newSquare = goalPath.GetSquare(over);
                    }
                    else
                    {
                        Console.WriteLine($"{chosenColor} piece has FINISHED its journey!");
                        break;
                    }
                }

                board.UpdatePiecePosition(pieceMarker, currentSquare, newSquare);
                pieceIndex = newIndex;
                currentSquare = newSquare;

                board.PrintBoard();
            }

            Console.WriteLine("Game ended. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
