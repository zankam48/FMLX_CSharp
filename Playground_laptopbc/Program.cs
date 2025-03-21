using System;

namespace ChessConsole
{
    public enum PieceColor { White, Black }

    public abstract class Piece
    {
        public PieceColor Color { get; protected set; }
        public char Symbol { get; protected set; }
        public bool HasMoved { get; set; } = false;

        public Piece(PieceColor color)
        {
            Color = color;
        }

        // Checks if the move from "from" to "to" is valid for this piece.
        public abstract bool IsValidMove(Board board, Position from, Position to);
    }

    public class King : Piece
    {
        public King(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♔' : '♚';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            int rowDiff = Math.Abs(to.Row - from.Row);
            int colDiff = Math.Abs(to.Col - from.Col);

            // Castling: king moves two squares horizontally
            if (!HasMoved && rowDiff == 0 && colDiff == 2)
            {
                // Cannot castle if currently in check
                if (board.IsSquareAttacked(from, OpponentColor()))
                    return false;

                // Determine rook positions and check that path is clear
                if (to.Col == 6) // kingside
                {
                    if (board.Squares[from.Row, 5] != null || board.Squares[from.Row, 6] != null)
                        return false;
                    Piece rook = board.Squares[from.Row, 7];
                    if (rook == null || rook.HasMoved)
                        return false;
                    // Ensure squares the king passes through are not attacked.
                    if (board.IsSquareAttacked(new Position(from.Row, from.Col + 1), OpponentColor()) ||
                        board.IsSquareAttacked(to, OpponentColor()))
                        return false;
                    return true;
                }
                else if (to.Col == 2) // queenside
                {
                    if (board.Squares[from.Row, 1] != null || board.Squares[from.Row, 2] != null || board.Squares[from.Row, 3] != null)
                        return false;
                    Piece rook = board.Squares[from.Row, 0];
                    if (rook == null || rook.HasMoved)
                        return false;
                    if (board.IsSquareAttacked(new Position(from.Row, from.Col - 1), OpponentColor()) ||
                        board.IsSquareAttacked(to, OpponentColor()))
                        return false;
                    return true;
                }
                return false;
            }

            // Normal king move: one square in any direction
            if (rowDiff <= 1 && colDiff <= 1)
            {
                Piece target = board.Squares[to.Row, to.Col];
                if (target != null && target.Color == Color)
                    return false;
                return true;
            }
            return false;
        }

        private PieceColor OpponentColor()
        {
            return Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }
    }

    public class Queen : Piece
    {
        public Queen(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♕' : '♛';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            int rowDiff = to.Row - from.Row;
            int colDiff = to.Col - from.Col;
            // Valid if moving along a rank, file, or diagonal
            if (rowDiff == 0 || colDiff == 0 || Math.Abs(rowDiff) == Math.Abs(colDiff))
            {
                int stepRow = rowDiff == 0 ? 0 : rowDiff / Math.Abs(rowDiff);
                int stepCol = colDiff == 0 ? 0 : colDiff / Math.Abs(colDiff);
                int currentRow = from.Row + stepRow;
                int currentCol = from.Col + stepCol;
                while (currentRow != to.Row || currentCol != to.Col)
                {
                    if (board.Squares[currentRow, currentCol] != null)
                        return false;
                    currentRow += stepRow;
                    currentCol += stepCol;
                }
                Piece target = board.Squares[to.Row, to.Col];
                if (target != null && target.Color == Color)
                    return false;
                return true;
            }
            return false;
        }
    }

    public class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♖' : '♜';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            // Must move in a straight line
            if (from.Row != to.Row && from.Col != to.Col)
                return false;
            int rowDiff = to.Row - from.Row;
            int colDiff = to.Col - from.Col;
            int stepRow = rowDiff == 0 ? 0 : rowDiff / Math.Abs(rowDiff);
            int stepCol = colDiff == 0 ? 0 : colDiff / Math.Abs(colDiff);
            int currentRow = from.Row + stepRow;
            int currentCol = from.Col + stepCol;
            while (currentRow != to.Row || currentCol != to.Col)
            {
                if (board.Squares[currentRow, currentCol] != null)
                    return false;
                currentRow += stepRow;
                currentCol += stepCol;
            }
            Piece target = board.Squares[to.Row, to.Col];
            if (target != null && target.Color == Color)
                return false;
            return true;
        }
    }

    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♗' : '♝';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            int rowDiff = to.Row - from.Row;
            int colDiff = to.Col - from.Col;
            if (Math.Abs(rowDiff) != Math.Abs(colDiff))
                return false;
            int stepRow = rowDiff / Math.Abs(rowDiff);
            int stepCol = colDiff / Math.Abs(colDiff);
            int currentRow = from.Row + stepRow;
            int currentCol = from.Col + stepCol;
            while (currentRow != to.Row)
            {
                if (board.Squares[currentRow, currentCol] != null)
                    return false;
                currentRow += stepRow;
                currentCol += stepCol;
            }
            Piece target = board.Squares[to.Row, to.Col];
            if (target != null && target.Color == Color)
                return false;
            return true;
        }
    }

    public class Knight : Piece
    {
        public Knight(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♘' : '♞';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            int rowDiff = Math.Abs(to.Row - from.Row);
            int colDiff = Math.Abs(to.Col - from.Col);
            if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
            {
                Piece target = board.Squares[to.Row, to.Col];
                if (target != null && target.Color == Color)
                    return false;
                return true;
            }
            return false;
        }
    }

    public class Pawn : Piece
    {
        public Pawn(PieceColor color) : base(color)
        {
            Symbol = color == PieceColor.White ? '♙' : '♟';
        }
        public override bool IsValidMove(Board board, Position from, Position to)
        {
            int direction = (Color == PieceColor.White) ? -1 : 1;
            int startRow = (Color == PieceColor.White) ? 6 : 1;
            int rowDiff = to.Row - from.Row;
            int colDiff = Math.Abs(to.Col - from.Col);

            Piece target = board.Squares[to.Row, to.Col];
            // Move forward one
            if (colDiff == 0)
            {
                if (rowDiff == direction && target == null)
                    return true;
                // Two-step move from starting position
                if (from.Row == startRow && rowDiff == 2 * direction && target == null)
                {
                    int intermediateRow = from.Row + direction;
                    if (board.Squares[intermediateRow, from.Col] == null)
                        return true;
                }
            }
            // Diagonal capture
            if (colDiff == 1 && rowDiff == direction)
            {
                // Normal capture
                if (target != null && target.Color != Color)
                    return true;
                // En passant capture
                if (target == null && board.EnPassantTarget != null &&
                    board.EnPassantTarget.Row == to.Row && board.EnPassantTarget.Col == to.Col)
                    return true;
            }
            return false;
        }
    }

    public class Position
    {
        // Row: 0 (top, rank 8) to 7 (bottom, rank 1)
        // Col: 0 (file a) to 7 (file h)
        public int Row { get; set; }
        public int Col { get; set; }
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
        // Parses standard chess notation (e.g., "e2"). Returns row and col in 0-indexed form.
        public static bool TryParse(string input, out Position pos)
        {
            pos = null;
            if (input.Length != 2)
                return false;
            char file = char.ToLower(input[0]);
            char rank = input[1];
            if (file < 'a' || file > 'h')
                return false;
            if (rank < '1' || rank > '8')
                return false;
            int col = file - 'a';
            int row = 8 - (rank - '0'); // Rank 8 -> row 0; Rank 1 -> row 7.
            pos = new Position(row, col);
            return true;
        }
        public override bool Equals(object obj)
        {
            if (obj is Position other)
                return this.Row == other.Row && this.Col == other.Col;
            return false;
        }
        public override int GetHashCode() => Row * 8 + Col;
    }

    public class Board
    {
        public Piece[,] Squares = new Piece[8, 8];
        // Stores the square that can be captured en passant (if any)
        public Position EnPassantTarget { get; set; } = null;

        public Board()
        {
            SetupBoard();
        }

        public void SetupBoard()
        {
            // Place pawns
            for (int col = 0; col < 8; col++)
            {
                Squares[6, col] = new Pawn(PieceColor.White);
                Squares[1, col] = new Pawn(PieceColor.Black);
            }
            // Place rooks
            Squares[7, 0] = new Rook(PieceColor.White);
            Squares[7, 7] = new Rook(PieceColor.White);
            Squares[0, 0] = new Rook(PieceColor.Black);
            Squares[0, 7] = new Rook(PieceColor.Black);
            // Place knights
            Squares[7, 1] = new Knight(PieceColor.White);
            Squares[7, 6] = new Knight(PieceColor.White);
            Squares[0, 1] = new Knight(PieceColor.Black);
            Squares[0, 6] = new Knight(PieceColor.Black);
            // Place bishops
            Squares[7, 2] = new Bishop(PieceColor.White);
            Squares[7, 5] = new Bishop(PieceColor.White);
            Squares[0, 2] = new Bishop(PieceColor.Black);
            Squares[0, 5] = new Bishop(PieceColor.Black);
            // Place queens
            Squares[7, 3] = new Queen(PieceColor.White);
            Squares[0, 3] = new Queen(PieceColor.Black);
            // Place kings
            Squares[7, 4] = new King(PieceColor.White);
            Squares[0, 4] = new King(PieceColor.Black);
        }

        // Draws the board using Unicode symbols. Empty squares are drawn with alternating characters.
        public void PrintBoard()
        {
            Console.Clear();
            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + " ");
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = Squares[row, col];
                    if (piece != null)
                        Console.Write(piece.Symbol + " ");
                    else
                        Console.Write(((row + col) % 2 == 0 ? "□" : "■") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        // Returns true if the given square is attacked by any piece of attackerColor.
        public bool IsSquareAttacked(Position pos, PieceColor attackerColor)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece p = Squares[row, col];
                    if (p != null && p.Color == attackerColor)
                    {
                        if (p.IsValidMove(this, new Position(row, col), pos))
                            return true;
                    }
                }
            }
            return false;
        }

        // Moves a piece from "from" to "to" if valid. Returns true if the move is successful.
        public bool MovePiece(Position from, Position to, PieceColor currentTurn)
        {
            Piece piece = Squares[from.Row, from.Col];
            if (piece == null || piece.Color != currentTurn)
            {
                Console.WriteLine("No valid piece at source.");
                return false;
            }
            if (!piece.IsValidMove(this, from, to))
            {
                Console.WriteLine("Invalid move for this piece.");
                return false;
            }

            // Backup state for simulation
            Piece captured = Squares[to.Row, to.Col];
            Position originalEnPassant = EnPassantTarget;
            bool originalHasMoved = piece.HasMoved;

            // Handle en passant capture for pawn
            if (piece is Pawn && from.Col != to.Col && captured == null)
            {
                // en passant: capture the pawn behind the target square
                int capturedRow = piece.Color == PieceColor.White ? to.Row + 1 : to.Row - 1;
                captured = Squares[capturedRow, to.Col];
                Squares[capturedRow, to.Col] = null;
            }

            // Execute move
            Squares[to.Row, to.Col] = piece;
            Squares[from.Row, from.Col] = null;
            piece.HasMoved = true;

            // Check if move leaves own king in check
            if (IsInCheck(currentTurn))
            {
                // Undo move
                Squares[from.Row, from.Col] = piece;
                Squares[to.Row, to.Col] = captured;
                // If en passant, restore captured pawn
                if (piece is Pawn && from.Col != to.Col && captured == null)
                {
                    int capturedRow = piece.Color == PieceColor.White ? to.Row + 1 : to.Row - 1;
                    // (Assume a pawn was captured; in a robust engine, you'd store it)
                }
                piece.HasMoved = originalHasMoved;
                EnPassantTarget = originalEnPassant;
                Console.WriteLine("Move would leave king in check.");
                return false;
            }

            // Handle pawn special moves
            if (piece is Pawn)
            {
                // If pawn moved two steps, mark en passant target
                if (Math.Abs(to.Row - from.Row) == 2)
                {
                    int direction = piece.Color == PieceColor.White ? -1 : 1;
                    EnPassantTarget = new Position(from.Row + direction, from.Col);
                }
                else
                {
                    EnPassantTarget = null;
                }
                // Pawn promotion if reaching last rank
                if ((piece.Color == PieceColor.White && to.Row == 0) ||
                    (piece.Color == PieceColor.Black && to.Row == 7))
                {
                    Console.WriteLine("Promote pawn (Q, R, B, N): ");
                    string choice = Console.ReadLine().ToUpper();
                    Piece newPiece = choice switch
                    {
                        "R" => new Rook(piece.Color),
                        "B" => new Bishop(piece.Color),
                        "N" => new Knight(piece.Color),
                        _ => new Queen(piece.Color),
                    };
                    Squares[to.Row, to.Col] = newPiece;
                }
            }
            else
            {
                EnPassantTarget = null;
            }

            // Handle castling: if king moves two squares, also move the rook.
            if (piece is King && Math.Abs(to.Col - from.Col) == 2)
            {
                if (to.Col == 6) // kingside
                {
                    Piece rook = Squares[from.Row, 7];
                    Squares[from.Row, 5] = rook;
                    Squares[from.Row, 7] = null;
                    if (rook != null) rook.HasMoved = true;
                }
                else if (to.Col == 2) // queenside
                {
                    Piece rook = Squares[from.Row, 0];
                    Squares[from.Row, 3] = rook;
                    Squares[from.Row, 0] = null;
                    if (rook != null) rook.HasMoved = true;
                }
            }
            return true;
        }

        // Returns true if the king of the given color is in check.
        public bool IsInCheck(PieceColor color)
        {
            Position kingPos = null;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece p = Squares[row, col];
                    if (p != null && p is King && p.Color == color)
                    {
                        kingPos = new Position(row, col);
                        break;
                    }
                }
                if (kingPos != null)
                    break;
            }
            if (kingPos == null)
                return true; // Should not happen

            PieceColor opponent = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            return IsSquareAttacked(kingPos, opponent);
        }

        // Returns true if any valid move exists for the given color.
        public bool HasAnyValidMoves(PieceColor color)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece p = Squares[row, col];
                    if (p != null && p.Color == color)
                    {
                        for (int r = 0; r < 8; r++)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                Position from = new Position(row, col);
                                Position to = new Position(r, c);
                                if (p.IsValidMove(this, from, to))
                                {
                                    // simulate move
                                    Piece temp = Squares[r, c];
                                    Squares[r, c] = p;
                                    Squares[row, col] = null;
                                    bool inCheck = IsInCheck(color);
                                    // undo simulation
                                    Squares[row, col] = p;
                                    Squares[r, c] = temp;
                                    if (!inCheck)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            PieceColor currentTurn = PieceColor.White;
            while (true)
            {
                board.PrintBoard();
                if (board.IsInCheck(currentTurn))
                {
                    if (!board.HasAnyValidMoves(currentTurn))
                    {
                        Console.WriteLine("Checkmate! " + (currentTurn == PieceColor.White ? "Black" : "White") + " wins!");
                        break;
                    }
                    Console.WriteLine("Check!");
                }
                else
                {
                    if (!board.HasAnyValidMoves(currentTurn))
                    {
                        Console.WriteLine("Stalemate!");
                        break;
                    }
                }
                Console.WriteLine(currentTurn + " to move. Enter move (e.g., e2e4): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.Replace(" ", "").Length < 4)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                input = input.Replace(" ", "").ToLower();
                string fromStr = input.Substring(0, 2);
                string toStr = input.Substring(2, 2);
                if (!Position.TryParse(fromStr, out Position from) || !Position.TryParse(toStr, out Position to))
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }
                if (!board.MovePiece(from, to, currentTurn))
                {
                    Console.WriteLine("Move failed, try again.");
                    continue;
                }
                currentTurn = currentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
            }
            Console.WriteLine("Game over. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
