public class Player
{
    public string name;

    public bool canMakeMove;
    private int score;
    public List<Piece> pieces = new List<Piece>();

    public Player(string name)
    {
        this.name = name;

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

}