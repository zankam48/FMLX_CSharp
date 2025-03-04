using Newtonsoft.Json;
public class Gacha
{
    static private int _pity;
    static private int _maxPity = 90;
    private static readonly Random random = new Random();

    public static string Roll()
    {
        int index = random.Next(0, 3); 
        // return items[index]; 
        return "pass";
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Element { get; set; }
    public string Rarity { get; set; }
    public string Description { get; set; }
}

Dictionary<string, double> rarityChances = new Dictionary<string, int>()
{
    {"5 Star", 0.01},
    {"4 Star", 0.10},
    {"3 Star", 0.80}
}