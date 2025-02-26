public interface IAction
{
    void Execute(Character character, Enemy enemy);
}

public class AttackAction : IAction
{
    public void Execute(Character character, Enemy enemy)
    {
        Console.WriteLine("Attacking...");
    }
}

public class HealAction : IAction
{
    public void Execute(Character character, Enemy enemy)
    {
        Console.WriteLine("Healing...");
    }
}