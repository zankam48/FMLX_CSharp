using System;
using System.Collections.Generic;

public class Hero
{
    public string Name;
    public string Ability;
    public Hero(string name, string ability)
    {
        Name = name;
        Ability = ability;
    }
}

public class NameAbilityEqComparer : EqualityComparer<Hero>
{
    public override bool Equals(Hero x, Hero y)
    {
        return x.Name == y.Name && x.Ability == y.Ability;
    }

    public override int GetHashCode(Hero obj)
    {
        return obj.Name.GetHashCode() ^ obj.Ability.GetHashCode();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Hero hero1 = new Hero("Hero1", "Ability1");
        Hero hero2 = new Hero("Hero1", "Ability1");

        Console.WriteLine(hero1 == hero2);

        var eqComparer = new NameAbilityEqComparer();
        var d = new Dictionary<Hero, string>(eqComparer);
        d[hero1] = "Hero1";

        Console.WriteLine(d.ContainsKey(hero2)); // True
    }
}