public class Dice
{
    public int diceValue { get; set; }
    public int Roll()
    {
        Random random = new Random();
        diceValue = random.Next(1, 7);
        return diceValue;
    }
}