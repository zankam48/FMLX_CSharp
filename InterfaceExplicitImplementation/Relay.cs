public class Relay : IActuator
{
    public bool IsActivated { get; set; }

    public void Activate()
    {
        IsActivated = true;
        Console.WriteLine("Relay activated.");
    }

    public void Deactivate()
    {
        IsActivated = false;
        Console.WriteLine("Relay deactivated.");
    }

    public string Info()
    {
        return $"Relay";
    }
}