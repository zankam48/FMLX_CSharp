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

    public bool IsOverlapped(Piece piece1, Piece piece2)
    {
        return piece1.Position == piece2.Position && piece1.Player != piece2.Player;
    }
    
}