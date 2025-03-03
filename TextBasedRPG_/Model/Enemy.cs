using System;

namespace TextBasedRPG;

public class Enemy : Character
{
    public Enemy(string Name, int AttackPower): base(Name, AttackPower)
    {
        string name = Name;
        int attackPower = AttackPower;
    }
    public override void TakeDamage(int damage)
    {}
}
