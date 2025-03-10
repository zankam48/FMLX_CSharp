namespace LudoGame.Controller;
using LudoGame.Enums;
using LudoGame.Interface;

public class GameController
{
    // Fields
    private List<Player> players;
    private IDice dice;
    public Player currentPlayer;
    public int currentPlayerIndex;

    // Constructor
    public GameController(List<Player> players, IDice dice)
    {
        this.players = players;
        this.dice = dice;
        this.currentPlayerIndex = 0;  // Start with the first player
        this.currentPlayer = players[currentPlayerIndex];
    }

    // Get pieces status for displaying in Program
    public string GetPieceStatus(IPiece piece)
    {
        return piece.Status == PieceStatus.AT_HOME ? "At Home" : $"Position {piece.Position.Row}";
    }

    // MovePiece method
    public bool MovePiece(IPiece piece, int diceValue)
    {
        if (piece is Piece && ((Piece)piece).Color != currentPlayer.Color)
        {
            return false;  // Invalid move (not the current player's piece)
        }

        if (piece.Status == PieceStatus.AT_HOME)
        {
            if (diceValue != 6)
            {
                return false;  // Cannot move if at home without a 6
            }
            else
            {
                piece.Status = PieceStatus.IN_PLAY;
                piece.Position = new Position(0, 0);  // Starting position
                return true;  // Piece moved out of home
            }
        }

        if (piece.Status == PieceStatus.IN_PLAY)
        {
            int newRow = piece.Position.Row + diceValue;
            piece.Position = new Position(newRow, piece.Position.Column);
            return true;  // Move successful
        }

        return false;  // Move not allowed
    }

    // NextPlayerTurn method
    public void NextPlayerTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        currentPlayer = players[currentPlayerIndex];
    }

    // RollDice method
    public int RollDice()
    {
        return dice.Roll();
    }
}
