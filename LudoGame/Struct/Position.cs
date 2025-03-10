public struct Position
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override bool Equals(object obj)
    {
        if (obj is Position)
        {
            var other = (Position)obj;
            return Row == other.Row && Column == other.Column;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Row.GetHashCode() ^ Column.GetHashCode();
    }
}