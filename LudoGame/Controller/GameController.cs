namespace LudoGame.Controller;
using LudoGame.Enums;
using LudoGame.Interface;
using System;
using System.Collections.Generic;

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
        return piece.Status == PieceStatus.AT_HOME ? "At Home" :
               piece.Status == PieceStatus.AT_GOAL ? "At Goal" :
               $"Position {piece.Position.Row}";
    }

    // Check if a player has any valid moves
    public bool CanPlayerMove(Player player, int rollValue)
    {
        foreach (var piece in player.Pieces)
        {
            if (piece.Status == PieceStatus.IN_PLAY) return true; // Has at least one piece in play
            if (piece.Status == PieceStatus.AT_HOME && rollValue == 6) return true; // Can bring a piece out
        }
        return false; // No valid moves
    }

    // Check if a player has at least one piece in play
    public bool HasPieceInPlay(Player player)
    {
        foreach (var piece in player.Pieces)
        {
            if (piece.Status == PieceStatus.IN_PLAY) return true;
        }
        return false;
    }

    // MovePiece method (Ensures Only Valid Moves)
    public bool MovePiece(IPiece piece, int diceValue)
{
    if (piece is Piece && ((Piece)piece).Color != currentPlayer.Color)
    {
        return false; // Invalid move (wrong player's piece)
    }

    // If piece is at home and rolling 6, move it out but don't apply the dice roll value yet
    if (piece.Status == PieceStatus.AT_HOME && diceValue == 6)
    {
        piece.Status = PieceStatus.IN_PLAY;
        piece.Position = new Position(0, 0); // Move to position 0, NOT 6
        return true;
    }

    // If piece is already in play, move it forward normally
    if (piece.Status == PieceStatus.IN_PLAY)
    {
        int newRow = piece.Position.Row + diceValue;  // Move forward by dice roll
        piece.Position = new Position(newRow, piece.Position.Column);
        return true;
    }

    return false; // Move not allowed
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

    // RollDice method (Now Requires Player Input)
    public int RollDice()
    {
        Console.WriteLine("🎲 Press any key to roll the dice...");
        Console.ReadKey(true);
        return OnDiceRoll != null ? OnDiceRoll((Dice)dice) : dice.Roll();
    }
}
