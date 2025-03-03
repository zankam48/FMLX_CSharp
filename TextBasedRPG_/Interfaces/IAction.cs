using TextBasedRPG;

public interface IAction
{
    void Execute(Character char1, IDamageable target);
}

public class AttackAction : IAction
{
    public void Execute(Character char1, IDamageable target, string type2)
    {
        Console.WriteLine("Attacking...");
    }

    public void Execute(Character char1, IDamageable target)
    {
        throw new NotImplementedException();
    }
}

// public class 