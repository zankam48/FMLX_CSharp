using LudoGame.Interface;
using LudoGame.Enums;
public class Piece : IPiece
{
    // Fields
    private PieceColor color;
    private Position position;
    private PieceStatus status;

    // Constructor
    public Piece(PieceColor color)
    {
        this.color = color;
        this.position = new Position(-1, -1); // No position initially
        this.status = PieceStatus.AT_HOME; // Initially at home
    }

    // Properties
    public PieceColor Color => color;
    public Position Position { get; set; }
    public PieceStatus Status { get; set; }

    // Methods
    public void UpdateStatus(PieceStatus status)
    {
        this.Status = status;
    }
}