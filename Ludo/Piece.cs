// class Piece {
//         -Dice dice
//         -PieceColor color
//         -position : int
//         +Piece(color: PieceColor, position: int)
//         +MoveWithValue(int diceValue) : void
//         +isAtGoal() : bool
//         +isAtHome() : bool
        
//     }

public enum PieceColor
{
    GREEN,
    BLUE,
    RED,
    YELLOW
}

public class Piece
{
    string? name;
    public PieceColor Color { get; set; }
    public int Position { get; set; }
    public Player? Player { get; set; }

    // public Piece(PieceColor color, int position)
    // {}

    public Piece(string name)
    {
        this.name = name;
    }

    public bool IsAtHome()
    {
        return true;
    }

    public bool IsAtGoal()
    {
        return true;
    }


    
}