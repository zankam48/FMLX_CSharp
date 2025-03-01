public class VoltageSensor : ISensor
{
    private double _voltage;
    public double Voltage { get; set; }

    public void ReadValue()
    {
        Console.WriteLine("Reading voltage value...");
        Voltage = 220.0;
        Console.WriteLine($"Voltage: {Voltage}V");
    }

    public string Info()
    {
        return $"Voltage Sensor: Voltage = {Voltage}";
    }

}