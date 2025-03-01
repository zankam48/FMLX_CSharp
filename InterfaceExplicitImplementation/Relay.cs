using IoTInterfaces;

public class Relay : IActuator
{
    public bool IsActivated { get; set; }

    void IActuator.Activate()
    {
        IsActivated = true;
        Console.WriteLine("Relay activated.");
    }

    void IActuator.Deactivate()
    {
        IsActivated = false;
        Console.WriteLine("Relay deactivated.");
    }

    string IActuator.Info()
    {
        return $"Relay";
    }
}