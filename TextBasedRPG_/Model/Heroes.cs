using System;

namespace TextBasedRPG;

public class Heroes : Character
{
    private bool _isDead;

    public Heroes(string name, int attackPower, ElementType element)
    {
        Name = name;
        AttackPower = attackPower;
        Element = element;
    }
    public override void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} has taken {damage} damage, and now has {Health} health.");
        if (Health <= 0)
        {
            Console.WriteLine($"{Name} has been defeated!");
        }
    }
}


