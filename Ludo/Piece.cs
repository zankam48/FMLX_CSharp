public class Piece
{
    public PieceColor Color { get; set; }
    public Position Position { get; set; }
    private bool atGoal = false;

    public Piece(PieceColor color, Position position)
    {
        Color = color;
        Position = position;
    }
    
    public void SetAtGoal()
    {
        atGoal = true;
    }
}

public struct Position
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override bool Equals(Object obj)
    {
        if (obj is Position other)
        {
            return Row == other.Row && Column == other.Column;
        } return false;
    }

    public override int GetHashCode()
    {
        return (Row, Column).GetHashCode();
    }
}