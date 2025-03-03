using Newtonsoft.Json;
public class Gacha
{
    private static readonly Random random = new Random();

    public static string Roll()
    {
        int index = random.Next(0, 3); 
        // return items[index]; 
        return "pass";
    }
}