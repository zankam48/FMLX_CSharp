public interface ICharacter
{
    string Name {get;}
    int Health {get; set; }
    bool isAlive();
    void TakeDamage(int damage);
    void Heal();
}

public class Heroes : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public int AttackPower { get; set; }
    public int Defense { get; set; }

    public Heroes(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
        AttackPower = attackPower;
    }

    public bool isAlive()
    {
        return Health > 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Console.WriteLine($"{Name} is dead!");
        }
    }

    public void Heal()
    {
        Health = Math.Min(Health + 5, MaxHealth);
        Console.WriteLine($"{Name} healed!");
    }

    public virtual void showInfo()
    {
        Console.WriteLine("");
    }
}

public class Pyro: Heroes{
    public Pyro(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }

    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Pyro");
    }
}

public class Cryo: Heroes{
    public Cryo(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }
    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Cryo");
    }
}

public class Hydro: Heroes{
    public Hydro(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }
    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Hydro");
    }
}


