public class Enemy
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Resistance { get; set; }

    public Enemy(int health, int damage, int resistance)
    {
        Health = health;
        Damage = damage;
        Resistance = resistance;
    }
}

