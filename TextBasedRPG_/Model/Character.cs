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
    
    private int _health;
    private int _healthMax;

    // public int Id { get; set; }

    public abstract void TakeDamage(int damage);

}
