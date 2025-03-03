using System;

namespace TextBasedRPG;

public enum ElementType
{
    Cryo,
    Pyro,
    Hydro
}

public abstract class Character : IDamageable, IIDentifiable
{
    
    public required string Name { get; set; }
    public required string Id { get; set; }
    
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int AttackPower { get; set; }

    public Character(string Name, int AttackPower)
    {}

    public abstract void TakeDamage(int damage);

}
