public class Player
{
    public string name;

    public bool canMakeMove;

    public void MakeMove(int value)
    {
        Console.WriteLine("CanMakeMove now");
        canMakeMove = false; 
    }

}