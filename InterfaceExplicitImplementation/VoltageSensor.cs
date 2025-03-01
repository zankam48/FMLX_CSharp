using IoTInterfaces;

public class VoltageSensor : ISensor
{
    private double _voltage;
    public double Voltage { get; set; }

    void ISensor.ReadValue() // gk bisa pake access modifier 
    {
        Console.WriteLine("Reading voltage value...");
        Voltage = 220.0;
        Console.WriteLine($"Voltage: {Voltage}V");
    }

    string ISensor.Info()
    {
        return $"Voltage Sensor: Voltage = {Voltage}";
    }

}