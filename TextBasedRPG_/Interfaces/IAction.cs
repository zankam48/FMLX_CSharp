using TextBasedRPG;

public interface IAction
{
    void Execute(Character char1, IDamageable target);
}

public class BasicAttackAction : IAction
{
    public void Execute(Character char1, IDamageable target)
    {
        Console.WriteLine("Attacking...");
    }
}

public class ChargedAttackAction : IAction
{
    public void Execute(Character char1, IDamageable target)
    {
        Console.WriteLine("Charged attack...");
    }
}

public class UltimateAttackAction : IAction
{
    public void Execute(Character char1, IDamageable target)
    {
        Console.WriteLine("Using ultimate attack...");
    }
}