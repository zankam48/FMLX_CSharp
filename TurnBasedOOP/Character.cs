public class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public int AttackPower { get; set; }
    public int Defense { get; set; }

    public Character(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }

    // public void AttackPowerChanged(object sender, AttackPowerChangedEventArgs e)
    // public void Attack(){

    // }

    // public bool isAlive(){
    // }

    public void TakeDamage()
    {
        Health--;
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
}

public class Pyro: Character{
    public Pyro(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }
}

public class Cryo: Character{
    public Cryo(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }
}

public class Hydro: Character{
    public Hydro(string name, int health, int attackPower) : base(name, health, attackPower)
    {

    }
}

