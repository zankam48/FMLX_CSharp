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

    // Delegates
    public Func<Dice, int> OnDiceRoll; // bisa buat bonus moves
    //     gameC.OnDiceRoll = (dice) => {
    //     int result = dice.Roll();
    //     Console.WriteLine($"🎲 Rolled a {result}!");

    //     Custom Logic: Bonus move if rolling a 6
    //     if (result == 6) {
    //         giveextramove()
    //         or
    //         selectpiece to move
    //         then able to execute turn again (roll dice again)
    //         possible to have consecutive 6
    //     }
    //     else {
    //         just move then nextplayer turn
    //         skip turn or next turn prolly
    //     }
    //     Return the result of the roll
    //     return result;
    //     custom logs is also possible
    // };

    public Action<Player> OnNextPlayerTurn;
    // kl dari contoh sebelumnya bisa untuk notify (your turn blabla custom)
    // bisa tambahin helper method or nyambung ke method kek bool CanMoveAnyPiece or CanMove
    /*** 
    1. **Custom Notifications:**
    - Personalized messages for each player’s turn
    2. **Enhanced Game Flow:**
    - Automatically skips turns if a player can’t move.
    *Cleaner Code:**
    - Keeps `NextPlayerTurn` method focused on **just changing the turn**.
    ***/

    public delegate void HandleSixRollDelegate(IPlayer player, IPiece piece, int rollResult);
    public HandleSixRollDelegate OnSixRoll;
    /*** 
    public void HandleSixRoll(Player player, Piece piece, int rollResult) {
    if (OnSixRoll != null) {
        OnSixRoll(player, piece, rollResult);  // Trigger the delegate if it’s set
    } else {
        // Default behavior if no custom logic
        Console.WriteLine($"{player.GetName()} rolled a 6! Move a piece or bring one out of home.");
    }
    }
    game.OnSixRoll = (player, piece, rollResult) => {
    Console.WriteLine($"🎉 {player.GetName()} rolled a {rollResult}!");

    // 🟢 Bring a piece out of home if possible
    if (player.HasPieceAtHome()) {
        Console.WriteLine("🏠 Bringing a piece out of home!");
        player.AddPieceToStart();
    }
    // 🔄 Move an existing piece if all pieces are out
    else {
        Console.WriteLine("🚀 Moving an existing piece!");
        game.MovePiece(piece, rollResult);
    }

    // 🔁 Allow another roll
    Console.WriteLine("🔥 You get to roll again!");
    game.RollDice();
    };
    public void AddPieceToStart() {
    var piece = pieces.FirstOrDefault(p => p.status == PieceStatus.AT_HOME);
    if (piece != null) {
        piece.UpdateStatus(PieceStatus.IN_PLAY);
        Console.WriteLine("🚀 Piece is now on the board!");
    }
}
    ***/
    
    

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
