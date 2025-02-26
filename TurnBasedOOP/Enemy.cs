public class Enemy : ICharacter
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Resistance { get; set; }

    public string Name => throw new NotImplementedException();
    
    public Enemy(int health, int damage, int resistance)
    {
        Health = health;
        Damage = damage;
        Resistance = resistance;
    }

    public bool isAlive()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public void Heal()
    {
        throw new NotImplementedException();
    }
}

