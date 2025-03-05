using Newtonsoft.Json;

Dictionary<string, double> rarityChances = new Dictionary<string, double>()
{
    {"5 Star", 0.01},
    {"4 Star", 0.10},
    {"3 Star", 0.80},
};

// Item item = JsonConvert.DeserializeObject<Item>(jsonItem);
public class Gacha
{
    // static private int _pity;
    // static private int Pity { get; set; }
    // static private int _maxPity = 90;
    private static readonly Random random = new Random();

    public static string Roll()
    {
        int index = random.Next(0, 3); 
        // return items[index]; 
        return "pass";
    }

    public double GetRate(int pity)
    {
        if (pity < 75)
        {
            return 0.06;
        }
        else if (pity < 90 && pity >= 75)
        {
            return (pity - 75)*0.06 + 0.006;
        }
        else
        {
            return 1.00;
        }
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


