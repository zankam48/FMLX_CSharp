public interface ICharacter
{
    string Name {get;}
    int Health {get; set; }
    bool isAlive();
    void TakeDamage(int damage);
    void Heal();
    void ShowInfo();
    void GainExp(int exp);
    int LevelUp {set;}
}

public class Heroes : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public int AttackPower { get; set; }
    public int Defense { get; set; }


    public Heroes(string name)
    {
        Name = name;
        Health = 200;
        MaxHealth = 200;
        AttackPower = 100;
    }

    public int LevelUp
    {
        set
        {
            _level += value;
            
        }
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

    public virtual void ShowInfo()
    {
        Console.WriteLine("");
    }
}

public class Pyro: Heroes{
    public Pyro(string name) : base(name)
    {

    }

    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Pyro");
    }
}

public class Cryo: Heroes{
    public Cryo(string name) : base(name)
    {

    }
    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Cryo");
    }
}

public class Hydro: Heroes{
    public Hydro(string name) : base(name) 
    {

    }
    public override void showInfo()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Attack Power: {AttackPower}, Type: Hydro");
    }
}


