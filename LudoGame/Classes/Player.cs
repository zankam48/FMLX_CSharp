using LudoGame.Interface;
using LudoGame.Enums;

public class Player : IPlayer
{
    // Fields
    private string name;
    private PieceColor color;
    private IPiece[] pieces;
    private int score;

    // Constructor
    public Player(string name, PieceColor color)
    {
        this.name = name;
        this.color = color;
        this.score = 0; // Initial score is zero

        // Initialize pieces
        pieces = new Piece[4];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = new Piece(color);
        }
    }

    // Properties
    public string Name => name;
    public PieceColor Color => color;
    public IPiece[] Pieces => pieces; // 👈 Return IPiece[] instead of Piece[]
    public int Score => score;

    // Methods
    public PieceColor GetColor()
    {
        return color;
    }

    public int GetScore()
    {
        return score;
    }

    public bool HasPieceAtHome()
    {
        return pieces.Any(piece => piece.Status == PieceStatus.AT_HOME);
    }

    public void IncrementScore(int points)
    {
        score += points;
    }

    public Piece GetPieceAtHome()
    {
        return (Piece)pieces.FirstOrDefault(piece => piece.Status == PieceStatus.AT_HOME);
    }
}
