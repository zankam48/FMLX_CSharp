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

public class HealAction : IAction
{
    public void Execute(Heroes character, ICharacter target)
    {
        Console.WriteLine("Healing...");
    }
}