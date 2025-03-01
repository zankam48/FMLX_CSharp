using IoTInterfaces;

public class CurrentSensor : ISensor
{
    private double _current;
    public double Current { get; set; }

    void ISensor.ReadValue()
    {
        Console.WriteLine("Reading voltage value...");
        Current = 5.0;
        Console.WriteLine($"Current: {Current}A");
    }

    string ISensor.Info()
    {
        return $"Current Sensor";
    }
}