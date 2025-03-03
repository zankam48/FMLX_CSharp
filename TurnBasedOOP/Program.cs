Heroes heroes1 = new Heroes("Ayaka", 100, 15);
public interface IAction
{
    void Execute(Heroes heroes, ICharacter target);
}

public class AttackAction : IAction
{
    public void Execute(Heroes heroes, ICharacter target)
    {
        Console.WriteLine("Attacking...");
    }
}

public class GetAttacked : IAction
{
    public void Execute(Heroes heroes, ICharacter target)
    {
        Console.WriteLine("Getting attacked...");
        heroes.TakeDamage(target.AttackPower);
    }
}

public class HealAction : IAction
{
    public void Execute(Heroes heroes, ICharacter target)
    {
        Console.WriteLine("Healing...");
    }
}

// top level statements must precede namespace and type declarations