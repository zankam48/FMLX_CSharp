public class Player
{
    public string? name;

    public bool canMakeMove;
    public int Score { get; private set; }
    private List<Piece> pieces;

    public Player(string name, List<Piece> pieces)
    {
        this.name = name;
        this.pieces = pieces;
    }

    public void ChoosePiece(List<Piece> pieces)
    {
        int index;
        if (pieces.Count == 1)
        {
            index = 0;
        }
        else
        {
            Console.WriteLine("test");
            /***
            kl pieces.Counte > 1, readline = int num, return index = num;
            ***/
        }
    }

    public void SetColor(PieceColor color)
    {
        
    }

    public bool CheckWin()
    {
        return false;
    }

    public List<Piece> GetPieces()
    {
        return pieces;
    }


}