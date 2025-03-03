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
    public ElementType Element { get; set; }
    
    public double Health { get; set; }
    public double MaxHealth { get; set; }
    public double AttackPower { get; set; }

    // public Character(string Name, int AttackPower)
    // {}

    public abstract void TakeDamage(double damage);

}
