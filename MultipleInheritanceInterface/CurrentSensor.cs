public class CurrentSensor : ISensor
{
    private double _current;
    public double Current { get; set; }

    public void ReadValue()
    {
        Console.WriteLine("Reading voltage value...");
        Current = 5.0;
        Console.WriteLine($"Current: {Current}A");
    }

    public string SensorInfo()
    {
        return $"Current Sensor";
    }
}