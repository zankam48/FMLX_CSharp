// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;

// // Enums
// enum PieceColor
// {
//     YELLOW,
//     RED,
//     BLUE,
//     GREEN
// }

// enum GameState
// {
//     NOT_STARTED,
//     PLAYING,
//     FINISHED
// }

// enum PieceStatus
// {
//     AT_HOME,
//     IN_PLAY,
//     AT_GOAL
// }

// // Interfaces
// interface IPlayer
// {
//     string Name { get; }
//     PieceColor Color { get; }
//     List<Piece> Pieces { get; }
//     int Score { get; }
//     PieceColor GetColor();
//     int GetScore();
//     bool HasPieceAtHome();
// }

// interface IDisplay
// {
//     void DisplayBoard();
// }

// interface IDice
// {
//     int Roll();
// }

// interface IPiece
// {
//     PieceColor Color { get; }
//     Position Position { get; }
//     PieceStatus Status { get; }
// }

// // Structs
// struct Position
// {
//     public int Row;
//     public int Column;

//     public Position(int row, int column)
//     {
//         Row = row;
//         Column = column;
//     }

//     public override bool Equals(object obj)
//     {
//         if (!(obj is Position))
//             return false;

//         Position other = (Position)obj;
//         return Row == other.Row && Column == other.Column;
//     }

//     public override int GetHashCode()
//     {
//         return Row * 31 + Column;
//     }

//     public override string ToString()
//     {
//         return $"({Row},{Column})";
//     }
// }

// // Classes
// class Player : IPlayer
// {
//     public string Name { get; private set; }
//     public PieceColor Color { get; private set; }
//     public List<Piece> Pieces { get; private set; }
//     public int Score { get; private set; }

//     public Player(string name, PieceColor color)
//     {
//         Name = name;
//         Color = color;
//         Pieces = new List<Piece>();
//         Score = 0;

//         // Initialize 4 pieces for each player
//         for (int i = 0; i < 4; i++)
//         {
//             Pieces.Add(new Piece(color, new Position(-1, -1), PieceStatus.AT_HOME, i + 1));
//         }
//     }

//     public PieceColor GetColor()
//     {
//         return Color;
//     }

//     public int GetScore()
//     {
//         return Score;
//     }

//     public bool HasPieceAtHome()
//     {
//         return Pieces.Any(p => p.Status == PieceStatus.AT_HOME);
//     }

//     public void IncrementScore()
//     {
//         Score++;
//     }
// }

// class Piece : IPiece
// {
//     public PieceColor Color { get; private set; }
//     public Position Position { get; private set; }
//     public PieceStatus Status { get; private set; }
//     public int Number { get; private set; } // To identify individual pieces (1-4)

//     public Piece(PieceColor color, Position position, PieceStatus status, int number)
//     {
//         Color = color;
//         Position = position;
//         Status = status;
//         Number = number;
//     }

//     public void UpdateStatus(PieceStatus status)
//     {
//         Status = status;
//     }

//     public void UpdatePosition(Position position)
//     {
//         Position = position;
//     }

//     public override string ToString()
//     {
//         return $"{Color}{Number}";
//     }
// }

// class Square
// {
//     private List<Piece> occupants;
//     public int Row { get; private set; }
//     public int Col { get; private set; }
//     public bool IsSafe { get; set; }
//     public bool IsStart { get; set; }
//     public PieceColor? StartColor { get; set; }

//     public Square(int row, int col)
//     {
//         Row = row;
//         Col = col;
//         occupants = new List<Piece>();
//         IsSafe = false;
//         IsStart = false;
//         StartColor = null;
//     }

//     public bool IsOccupied()
//     {
//         return occupants.Count > 0;
//     }

//     public List<Piece> GetOccupants()
//     {
//         return new List<Piece>(occupants);
//     }

//     public void AddPiece(Piece piece)
//     {
//         occupants.Add(piece);
//     }

//     public void RemovePiece(Piece piece)
//     {
//         occupants.RemoveAll(p => p.Color == piece.Color && p.Number == piece.Number);
//     }

//     public Position GetPosition()
//     {
//         return new Position(Row, Col);
//     }
// }

// class Path
// {
//     private List<Square> squares;

//     public Path()
//     {
//         squares = new List<Square>();
//     }

//     public void AddSquare(Square square)
//     {
//         squares.Add(square);
//     }

//     public Square GetSquare(int index)
//     {
//         if (index >= 0 && index < squares.Count)
//             return squares[index];
//         return null;
//     }

//     public int Length()
//     {
//         return squares.Count;
//     }
// }

// class PathManager
// {
//     private Dictionary<PieceColor, Path> mainPaths;
//     private Dictionary<PieceColor, Path> goalPaths;
//     private Dictionary<PieceColor, Path> fullPaths;
//     private Dictionary<PieceColor, Position> homePositions;
//     private Dictionary<PieceColor, Position> startPositions;

//     public PathManager(Square[][] grid)
//     {
//         mainPaths = new Dictionary<PieceColor, Path>();
//         goalPaths = new Dictionary<PieceColor, Path>();
//         fullPaths = new Dictionary<PieceColor, Path>();
//         homePositions = new Dictionary<PieceColor, Position>();
//         startPositions = new Dictionary<PieceColor, Position>();

//         InitializeHomePositions();
//         InitializeStartPositions();
//         InitializeMainPaths(grid);
//         InitializeGoalPaths(grid);
//         InitializeFullPaths();
//     }

//     private void InitializeHomePositions()
//     {
//         homePositions[PieceColor.RED] = new Position(1, 1);
//         homePositions[PieceColor.GREEN] = new Position(1, 13);
//         homePositions[PieceColor.YELLOW] = new Position(13, 1);
//         homePositions[PieceColor.BLUE] = new Position(13, 13);
//     }

//     private void InitializeStartPositions()
//     {
//         startPositions[PieceColor.RED] = new Position(1, 6);
//         startPositions[PieceColor.GREEN] = new Position(6, 13);
//         startPositions[PieceColor.YELLOW] = new Position(8, 1);
//         startPositions[PieceColor.BLUE] = new Position(13, 8);
//     }

//     public Position GetHomePosition(PieceColor color)
//     {
//         return homePositions[color];
//     }

//     public Position GetStartPosition(PieceColor color)
//     {
//         return startPositions[color];
//     }

//     private void InitializeMainPaths(Square[][] grid)
//     {
//         // Common path all pieces traverse (52 squares)
//         Path commonPath = new Path();
        
//         // RED starts at (1,6) and moves right
//         for (int col = 6; col <= 14; col++)
//             commonPath.AddSquare(grid[1][col]);
            
//         // DOWN
//         for (int row = 2; row <= 6; row++)
//             commonPath.AddSquare(grid[row][14]);
            
//         // RIGHT to LEFT
//         for (int col = 13; col >= 8; col--)
//             commonPath.AddSquare(grid[7][col]);
            
//         // DOWN
//         for (int row = 8; row <= 13; row++)
//             commonPath.AddSquare(grid[row][7]);
            
//         // LEFT to RIGHT
//         for (int col = 6; col >= 0; col--)
//             commonPath.AddSquare(grid[13][col]);
            
//         // UP
//         for (int row = 12; row >= 8; row--)
//             commonPath.AddSquare(grid[row][0]);
            
//         // LEFT to RIGHT
//         for (int col = 1; col <= 6; col++)
//             commonPath.AddSquare(grid[7][col]);
            
//         // UP
//         for (int row = 6; row >= 2; row--)
//             commonPath.AddSquare(grid[row][7]);
            
//         // Skip last position as it overlaps with first

//         // Create main path for each color with different starting points
//         foreach (PieceColor color in Enum.GetValues(typeof(PieceColor)))
//         {
//             mainPaths[color] = new Path();
            
//             int offset = 0;
//             switch (color)
//             {
//                 case PieceColor.RED:
//                     offset = 0;
//                     break;
//                 case PieceColor.GREEN:
//                     offset = 13;
//                     break;
//                 case PieceColor.YELLOW:
//                     offset = 26;
//                     break;
//                 case PieceColor.BLUE:
//                     offset = 39;
//                     break;
//             }
            
//             // Create path with proper offset
//             for (int i = 0; i < 52; i++)
//             {
//                 int adjustedIndex = (i + offset) % 52;
//                 mainPaths[color].AddSquare(commonPath.GetSquare(adjustedIndex));
//             }
//         }
//     }

//     private void InitializeGoalPaths(Square[][] grid)
//     {
//         // Goal paths (6 squares each, including the final goal)
//         goalPaths[PieceColor.RED] = new Path();
//         for (int row = 2; row <= 6; row++)
//             goalPaths[PieceColor.RED].AddSquare(grid[row][7]);
//         goalPaths[PieceColor.RED].AddSquare(grid[7][7]); // Center goal
        
//         goalPaths[PieceColor.GREEN] = new Path();
//         for (int col = 12; col >= 8; col--)
//             goalPaths[PieceColor.GREEN].AddSquare(grid[7][col]);
//         goalPaths[PieceColor.GREEN].AddSquare(grid[7][7]); // Center goal
        
//         goalPaths[PieceColor.YELLOW] = new Path();
//         for (int row = 12; row >= 8; row--)
//             goalPaths[PieceColor.YELLOW].AddSquare(grid[row][7]);
//         goalPaths[PieceColor.YELLOW].AddSquare(grid[7][7]); // Center goal
        
//         goalPaths[PieceColor.BLUE] = new Path();
//         for (int col = 2; col <= 6; col++)
//             goalPaths[PieceColor.BLUE].AddSquare(grid[7][col]);
//         goalPaths[PieceColor.BLUE].AddSquare(grid[7][7]); // Center goal
//     }

//     private void InitializeFullPaths()
//     {
//         foreach (PieceColor color in Enum.GetValues(typeof(PieceColor)))
//         {
//             fullPaths[color] = new Path();
            
//             // Add main path
//             for (int i = 0; i < mainPaths[color].Length(); i++)
//             {
//                 fullPaths[color].AddSquare(mainPaths[color].GetSquare(i));
//             }
            
//             // Add goal path
//             for (int i = 0; i < goalPaths[color].Length(); i++)
//             {
//                 fullPaths[color].AddSquare(goalPaths[color].GetSquare(i));
//             }
//         }
//     }

//     public Path GetMainPath(PieceColor color)
//     {
//         return mainPaths[color];
//     }

//     public Path GetGoalPath(PieceColor color)
//     {
//         return goalPaths[color];
//     }

//     public Path GetFullPath(PieceColor color)
//     {
//         return fullPaths[color];
//     }
// }

// class Board
// {
//     private const int BOARD_SIZE = 15;
//     private Square[][] grid;
//     private Dictionary<Position, List<Piece>> pieceMap;
//     private PathManager pathManager;
//     private Dictionary<Tuple<PieceColor, int>, int> piecePositionIndices; // Tracks position in path for each piece

//     public Board()
//     {
//         InitializeGrid();
//         pieceMap = new Dictionary<Position, List<Piece>>();
//         piecePositionIndices = new Dictionary<Tuple<PieceColor, int>, int>();
//         pathManager = new PathManager(grid);
//         MarkSpecialSquares();
//     }

//     private void InitializeGrid()
//     {
//         grid = new Square[BOARD_SIZE][];
//         for (int i = 0; i < BOARD_SIZE; i++)
//         {
//             grid[i] = new Square[BOARD_SIZE];
//             for (int j = 0; j < BOARD_SIZE; j++)
//             {
//                 grid[i][j] = new Square(i, j);
//             }
//         }
//     }

//     private void MarkSpecialSquares()
//     {
//         // Mark safe squares
//         grid[1][6].IsSafe = true;  // Red start
//         grid[6][13].IsSafe = true; // Green start
//         grid[8][1].IsSafe = true;  // Yellow start
//         grid[13][8].IsSafe = true; // Blue start
        
//         // Additional safe squares
//         grid[1][8].IsSafe = true;
//         grid[6][1].IsSafe = true;
//         grid[8][13].IsSafe = true;
//         grid[13][6].IsSafe = true;
        
//         // Mark start squares with their colors
//         grid[1][6].IsStart = true;
//         grid[1][6].StartColor = PieceColor.RED;
        
//         grid[6][13].IsStart = true;
//         grid[6][13].StartColor = PieceColor.GREEN;
        
//         grid[8][1].IsStart = true;
//         grid[8][1].StartColor = PieceColor.YELLOW;
        
//         grid[13][8].IsStart = true;
//         grid[13][8].StartColor = PieceColor.BLUE;
//     }

//     public Square[][] GetGrid()
//     {
//         return grid;
//     }

//     public PathManager GetPathManager()
//     {
//         return pathManager;
//     }

//     public void PlacePieceAtHome(Piece piece)
//     {
//         Position homePos = pathManager.GetHomePosition(piece.Color);
//         UpdatePiecePosition(piece, homePos, -1); // -1 means at home, not on path
//         piece.UpdateStatus(PieceStatus.AT_HOME);
//     }

//     public void PlacePieceAtStart(Piece piece)
//     {
//         Position startPos = pathManager.GetStartPosition(piece.Color);
//         UpdatePiecePosition(piece, startPos, 0); // 0 is the start position in path
//         piece.UpdateStatus(PieceStatus.IN_PLAY);
//     }

//     public void MovePiece(Piece piece, int steps)
//     {
//         Tuple<PieceColor, int> key = new Tuple<PieceColor, int>(piece.Color, piece.Number);
        
//         if (!piecePositionIndices.ContainsKey(key))
//         {
//             // Piece is not on the board yet
//             if (steps == 6) // Can enter the board
//             {
//                 PlacePieceAtStart(piece);
//             }
//             return;
//         }
        
//         int currentIndex = piecePositionIndices[key];
//         int newIndex = currentIndex + steps;
        
//         Path fullPath = pathManager.GetFullPath(piece.Color);
        
//         if (newIndex >= fullPath.Length())
//         {
//             // Bounces back if overshoots
//             int overSteps = newIndex - (fullPath.Length() - 1);
//             newIndex = (fullPath.Length() - 1) - overSteps;
//         }
        
//         Square targetSquare = fullPath.GetSquare(newIndex);
        
//         // Check if target is final goal
//         if (newIndex == fullPath.Length() - 1)
//         {
//             UpdatePiecePosition(piece, targetSquare.GetPosition(), newIndex);
//             piece.UpdateStatus(PieceStatus.AT_GOAL);
//             return;
//         }
        
//         // Handle collisions
//         HandleCollision(piece, targetSquare);
        
//         // Update piece position
//         UpdatePiecePosition(piece, targetSquare.GetPosition(), newIndex);
//     }

//     public void HandleCollision(Piece movingPiece, Square targetSquare)
//     {
//         List<Piece> occupants = targetSquare.GetOccupants();
        
//         // If square is empty or safe, no collision
//         if (occupants.Count == 0 || targetSquare.IsSafe)
//             return;
            
//         // Check if opponent pieces are on target square
//         foreach (Piece occupant in occupants.ToList()) // Use ToList to avoid collection modified exception
//         {
//             if (occupant.Color != movingPiece.Color)
//             {
//                 // Send back to home
//                 targetSquare.RemovePiece(occupant);
//                 PlacePieceAtHome(occupant);
//                 Console.WriteLine($"{movingPiece} kicked {occupant} back to home!");
//             }
//         }
//     }

//     public void UpdatePiecePosition(Piece piece, Position newPosition, int pathIndex)
//     {
//         // Remove from current position if on board
//         if (piece.Position.Row != -1 && piece.Position.Column != -1)
//         {
//             if (pieceMap.ContainsKey(piece.Position))
//             {
//                 pieceMap[piece.Position].RemoveAll(p => p.Color == piece.Color && p.Number == piece.Number);
//                 if (pieceMap[piece.Position].Count == 0)
//                 {
//                     pieceMap.Remove(piece.Position);
//                 }
//             }
            
//             // Also remove from square
//             grid[piece.Position.Row][piece.Position.Column].RemovePiece(piece);
//         }
        
//         // Update piece's position
//         piece.UpdatePosition(newPosition);
        
//         // Add to new position
//         if (!pieceMap.ContainsKey(newPosition))
//         {
//             pieceMap[newPosition] = new List<Piece>();
//         }
//         pieceMap[newPosition].Add(piece);
        
//         // Add to square
//         if (newPosition.Row >= 0 && newPosition.Column >= 0)
//         {
//             grid[newPosition.Row][newPosition.Column].AddPiece(piece);
//         }
        
//         // Update path index
//         if (pathIndex >= 0)
//         {
//             Tuple<PieceColor, int> key = new Tuple<PieceColor, int>(piece.Color, piece.Number);
//             piecePositionIndices[key] = pathIndex;
//         }
//         else
//         {
//             // Remove from path indices if going back home
//             Tuple<PieceColor, int> key = new Tuple<PieceColor, int>(piece.Color, piece.Number);
//             if (piecePositionIndices.ContainsKey(key))
//             {
//                 piecePositionIndices.Remove(key);
//             }
//         }
//     }

//     public List<Piece> GetPiecesAt(Position position)
//     {
//         if (pieceMap.ContainsKey(position))
//         {
//             return new List<Piece>(pieceMap[position]);
//         }
//         return new List<Piece>();
//     }

//     public int GetPiecePathPosition(Piece piece)
//     {
//         Tuple<PieceColor, int> key = new Tuple<PieceColor, int>(piece.Color, piece.Number);
//         if (piecePositionIndices.ContainsKey(key))
//         {
//             return piecePositionIndices[key];
//         }
//         return -1; // Not on path
//     }

//     public List<Piece> GetMovablePieces(Player player, int diceRoll)
//     {
//         List<Piece> movablePieces = new List<Piece>();
        
//         foreach (Piece piece in player.Pieces)
//         {
//             if (piece.Status == PieceStatus.AT_GOAL)
//                 continue;
                
//             if (piece.Status == PieceStatus.AT_HOME)
//             {
//                 if (diceRoll == 6)
//                     movablePieces.Add(piece);
//                 continue;
//             }
            
//             // Piece is in play
//             int currentPosition = GetPiecePathPosition(piece);
//             int newPosition = currentPosition + diceRoll;
            
//             Path fullPath = pathManager.GetFullPath(piece.Color);
            
//             // Check if move is valid (not overshooting goal by too much)
//             if (newPosition < fullPath.Length() || 
//                 (newPosition >= fullPath.Length() && 
//                  newPosition - fullPath.Length() < fullPath.Length()))
//             {
//                 movablePieces.Add(piece);
//             }
//         }
        
//         return movablePieces;
//     }
// }

// class Display : IDisplay
// {
//     private Board board;
//     private List<Player> players;

//     public Display(Board board, List<Player> players)
//     {
//         this.board = board;
//         this.players = players;
//     }

//     public void DisplayBoard()
//     {
//         Console.Clear();
//         Square[][] grid = board.GetGrid();

//         Console.WriteLine("LUDO BOARD");
//         Console.WriteLine("==========");
        
//         // Display board with coordinates and pieces
//         for (int i = 0; i < grid.Length; i++)
//         {
//             for (int j = 0; j < grid[i].Length; j++)
//             {
//                 // Get pieces at current position
//                 Position pos = new Position(i, j);
//                 List<Piece> pieces = board.GetPiecesAt(pos);

//                 // Display special squares and pieces
//                 if (pieces.Count > 0)
//                 {
//                     // Show the first piece if multiple pieces
//                     Console.Write($"[{pieces[0]}]");
//                 }
//                 else if (i == 7 && j == 7) // Center/goal
//                 {
//                     Console.Write("[XX]");
//                 }
//                 else if (grid[i][j].IsSafe)
//                 {
//                     Console.Write("[SS]");
//                 }
//                 else if (grid[i][j].IsStart)
//                 {
//                     char colorChar = grid[i][j].StartColor.ToString()[0];
//                     Console.Write($"[{colorChar}S]");
//                 }
//                 else if ((i < 6 || i > 8) && (j < 6 || j > 8)) // Home areas
//                 {
//                     if ((i <= 5 && j <= 5) || // Red home
//                         (i <= 5 && j >= 9) || // Green home
//                         (i >= 9 && j <= 5) || // Yellow home
//                         (i >= 9 && j >= 9))   // Blue home
//                     {
//                         Console.Write("[  ]");
//                     }
//                     else
//                     {
//                         Console.Write("    ");
//                     }
//                 }
//                 else if (i == 7 || j == 7) // Path
//                 {
//                     Console.Write("[--]");
//                 }
//                 else
//                 {
//                     Console.Write("    ");
//                 }
//             }
//             Console.WriteLine();
//         }
        
//         // Display Player information
//         Console.WriteLine("\nPLAYERS:");
//         foreach (Player player in players)
//         {
//             Console.WriteLine($"{player.Name} ({player.Color}) - Score: {player.Score}");
//             Console.Write("  Pieces: ");
//             foreach (Piece piece in player.Pieces)
//             {
//                 Console.Write($"{piece} ({piece.Status})  ");
//             }
//             Console.WriteLine();
//         }
//     }
// }

// class Dice : IDice
// {
//     private Random random;

//     public Dice()
//     {
//         random = new Random();
//     }

//     public int Roll()
//     {
//         return random.Next(1, 7);
//     }
// }
/***
// class GameController
// {
//     private Board board;
//     private List<Player> players;
//     private IDice dice;
//     private int currentPlayerIndex;
//     private bool isGameOver;
//     private Display display;

//     // Delegates
//     public delegate void NextPlayerTurnHandler(Player player);
//     public delegate int DiceRollHandler(IDice dice);
//     public delegate void HandleSixRollDelegate(Player player, Piece piece, int rollResult);

//     // Events
//     public event NextPlayerTurnHandler OnNextPlayerTurn;
//     public event DiceRollHandler OnDiceRoll;
//     public event HandleSixRollDelegate OnSixRoll;

//     public GameController(List<Player> players, Board board, IDice dice, Display display)
***/
//     {
//         this.players = players;
//         this.board = board;
//         this.dice = dice;
//         this.display = display;
//         currentPlayerIndex = 0;
//         isGameOver = false;

//         // Initialize player pieces
//         foreach (Player player in players)
//         {
//             foreach (Piece piece in player.Pieces)
//             {
//                 board.PlacePieceAtHome(piece);
//             }
//         }
//     }

//     public void StartGame()
//     {
//         isGameOver = false;
//         currentPlayerIndex = 0;
//         display.DisplayBoard();
//         Console.WriteLine("Game started! Press any key to begin...");
//         Console.ReadKey();
        
//         while (!isGameOver)
//         {
//             ExecuteTurn();
//             CheckGameOver();
//             NextPlayerTurn();
//         }
        
//         EndGame();
//     }

//     public void EndGame()
//     {
//         Player winner = GetWinner();
//         Console.Clear();
//         display.DisplayBoard();
//         Console.WriteLine($"Game Over! {winner.Name} ({winner.Color}) wins!");
//         Console.WriteLine("Press any key to exit...");
//         Console.ReadKey();
//     }

//     public void NextPlayerTurn()
//     {
//         if (!isGameOver)
//         {
//             currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
//             OnNextPlayerTurn?.Invoke(players[currentPlayerIndex]);
//         }
//     }

//     public void ExecuteTurn()
//     {
//         Player currentPlayer = players[currentPlayerIndex];
        
//         display.DisplayBoard();
//         Console.WriteLine($"\n{currentPlayer.Name}'s turn ({currentPlayer.Color})");
//         Console.WriteLine("Press any key to roll the dice...");
//         Console.ReadKey();
        
//         int rollResult = RollDice();
//         Console.WriteLine($"{currentPlayer.Name} rolled a {rollResult}");
        
//         if (rollResult == 6)
//         {
//             HandleSixRoll(currentPlayer, null, rollResult);
//             return;
//         }
        
//         // Get movable pieces
//         List<Piece> movablePieces = board.GetMovablePieces(currentPlayer, rollResult);
        
//         if (movablePieces.Count == 0)
//         {
//             Console.WriteLine("No valid moves available. Turn skipped.");
//             Thread.Sleep(2000);
//             return;
//         }
        
//         // Let player select a piece to move
//         Piece selectedPiece = SelectPiece(currentPlayer, movablePieces);
//         if (selectedPiece != null)
//         {
//             MovePiece(selectedPiece, rollResult);
//             display.DisplayBoard();
//             Thread.Sleep(1000);
//         }
//         else
//         {
//             Console.WriteLine("No piece selected. Turn skipped.");
//             Thread.Sleep(2000);
//         }
//     }

//     private Piece SelectPiece(Player player, List<Piece> movablePieces)
//     {
//         if (movablePieces.Count == 0)
//             return null;
            
//         if (movablePieces.Count == 1)
//             return movablePieces[0];
            
//         Console.WriteLine("Select a piece to move:");
//         for (int i = 0; i < movablePieces.Count; i++)
//         {
//             Piece piece = movablePieces[i];
//             Console.WriteLine($"{i + 1}. {piece} (Status: {piece.Status})");
//         }
        
//         int selection = -1;
//         while (selection < 1 || selection > movablePieces.Count)
//         {
//             Console.Write("Enter piece number: ");
//             if (!int.TryParse(Console.ReadLine(), out selection))
//             {
//                 selection = -1;
//             }
//         }
        
//         return movablePieces[selection - 1];
//     }

//     public void MovePiece(Piece piece, int diceValue)
//     {
//         if (piece.Status == PieceStatus.AT_HOME && diceValue == 6)
//         {
//             board.PlacePieceAtStart(piece);
//             Console.WriteLine($"{piece} moved to start position");
//         }
//         else if (piece.Status == PieceStatus.IN_PLAY)
//         {
//             board.MovePiece(piece, diceValue);
//             Console.WriteLine($"{piece} moved {diceValue} steps");
            
//             // Check if piece reached goal
//             if (piece.Status == PieceStatus.AT_GOAL)
//             {
//                 Player player = players.First(p => p.Color == piece.Color);
//                 player.IncrementScore();
//                 Console.WriteLine($"{piece} reached the goal! {player.Name}'s score is now {player.Score}");
//             }
//         }
//     }

//     public bool IsOverlapped(Player player1, Player player2)
//     {
//         foreach (Piece piece1 in player1.Pieces)
//         {
//             foreach (Piece piece2 in player2.Pieces)
//             {
//                 if (piece1.Position.Equals(piece2.Position) && 
//                     piece1.Status == PieceStatus.IN_PLAY && 
//                     piece2.Status == PieceStatus.IN_PLAY)
//                 {
//                     return true;
//                 }
//             }
//         }
//         return false;
//     }

//     public void KickPiece(Piece piece)
//     {
//         board.PlacePieceAtHome(piece);
//     }

//     public int RollDice()
//     {
//         int result = dice.Roll();
//         if (OnDiceRoll != null)
//         {
//             result = OnDiceRoll(dice);
//         }
//         return result;
//     }

//     public void HandleSixRoll(Player player, Piece piece, int rollResult)
//     {
//         Console.WriteLine("You rolled a six! You get another turn.");
        
//         // If player has pieces at home, option to bring one out
//         if (player.HasPieceAtHome())
//         {
//             Console.WriteLine("Do you want to bring a piece out from home? (Y/N)");
//             char choice = Console.ReadKey().KeyChar;
//             Console.WriteLine();
            
//             if (choice == 'Y' || choice == 'y')
//             {
//                 // Find a piece at home
//                 Piece homePiece = player.Pieces.First(p => p.Status == PieceStatus.AT_HOME);
//                 board.PlacePieceAtStart(homePiece);
//                 Console.WriteLine($"{homePiece} moved to start position");
//                 display.DisplayBoard();
//                 Thread.Sleep(1000);
//             }
//             else
//             {
//                 // Move a piece already in play
//                 List<Piece> movablePieces = board.GetMovablePieces(player, rollResult)
//                     .Where(p => p.Status == PieceStatus.IN_PLAY)
//                     .ToList();
                    
//                 if (movablePieces.Count > 0)
//                 {
//                     Piece selectedPiece = SelectPiece(player, movabl