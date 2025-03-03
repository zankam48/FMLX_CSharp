using TextBasedRPG;

public interface IAction
{
    void Execute(Character char1, Character target);
}

public class BasicAttackAction : IAction
{
    public void Execute(Character char1, Character target)
    {
        Console.WriteLine("Attacking...");
    }
}

public class ElementalAttackAction : IAction
{
    public void Execute(Character attacker, Character target)
    {
        double damage = attacker.AttackPower / 10;
        ElementalReaction(attacker, target, ref damage);
        target.TakeDamage(damage);
    }

    private void ElementalReaction(Character attacker, Character target, ref double damage)
    {
        if (((attacker.Element == ElementType.Cryo) && (target.Element == ElementType.Pyro)) || ((attacker.Element == ElementType.Cryo) && (target.Element == ElementType.Pyro)))
        {
            damage *= 2; // melt
        } 
        if (((attacker.Element == ElementType.Cryo) && (target.Element == ElementType.Hydro)) || ((attacker.Element == ElementType.Hydro) && (target.Element == ElementType.Cryo)))
        {
            damage *= 1.5;
            target.Health -= 4;
        }
        if (((attacker.Element == ElementType.Cryo) && (target.Element == ElementType.Hydro)) || ((attacker.Element == ElementType.Hydro) && (target.Element == ElementType.Cryo)))
        {
            damage *= 1.7;
            target.Health -= 2;
        }
    }
}

public class UltimateAttackAction : IAction
{
    public void Execute(Character char1, Character target)
    {
        Console.WriteLine("Using ultimate attack...");
    }
}