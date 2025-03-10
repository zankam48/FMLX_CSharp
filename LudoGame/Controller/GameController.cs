namespace LudoGame.Controller;
using LudoGame.Enums;
using LudoGame.Interface;
using System;

public class GameController
{
    // Fields
    private List<Player> players;
    private IDice dice;
    public Player currentPlayer;
    public int currentPlayerIndex;

    // Delegates
    public Func<Dice, int> OnDiceRoll;
    public Action<Player> OnNextPlayerTurn;
    public delegate void HandleSixRollDelegate(IPlayer player, IPiece piece, int rollResult);
    public HandleSixRollDelegate OnSixRoll;

    // Constructor
    public GameController(List<Player> players, IDice dice)
    {
        this.players = players;
        this.dice = dice;
        this.currentPlayerIndex = 0;
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
            return false;  // Invalid move
        }

        if (piece.Status == PieceStatus.AT_HOME && diceValue == 6)
        {
            piece.Status = PieceStatus.IN_PLAY;
            piece.Position = new Position(0, 0);  // Move out of home to start position
            return true;
        }
        else if (piece.Status == PieceStatus.AT_HOME)
        {
            return false;  // Can't move out of home without a 6
        }

        if (piece.Status == PieceStatus.IN_PLAY)
        {
            int newRow = piece.Position.Row + diceValue;
            piece.Position = new Position(newRow, piece.Position.Column);
            return true;
        }

        return false;  // Move not allowed
    }

    // Handle rolling a six
    public void HandleSixRoll(IPiece piece, int rollResult)
    {
        if (rollResult == 6)
        {
            OnSixRoll?.Invoke(currentPlayer, piece, rollResult);
        }
    }

    // NextPlayerTurn method
    public void NextPlayerTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        currentPlayer = players[currentPlayerIndex];

        // Trigger delegate if assigned
        OnNextPlayerTurn?.Invoke(currentPlayer);
    }

    // RollDice method
    public int RollDice()
    {
        // Use delegate if set, else default dice roll
        return OnDiceRoll != null ? OnDiceRoll((Dice)dice) : dice.Roll();
    }
}
