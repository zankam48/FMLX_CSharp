using LudoGame.Interface;
public class Dice : IDice
{
    private Random random;

    // Constructor
    public Dice()
    {
        random = new Random();
    }

    // Roll method: returns a random number between 1 and 6
    public int Roll()
    {
        return random.Next(1, 7); // 7 is exclusive, so this gives 1 to 6
    }

    // Method to display roll result for a player
    public void DisplayRollResult(Player player)
    {
        int rollValue = Roll();
        Console.WriteLine($"Player {player.Name} rolled a {rollValue}.");
    }
}