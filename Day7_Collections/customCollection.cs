using System;
using System.Collections.ObjectModel;


public class Sensor
{
    public string Name { get; set; }
    public double Value { get; set; }
    public SmartIOT smartIOT { get; set; }

    public Sensor(string name, double value, SmartIOT smartIOT)
    {
        Name = name;
        Value = value;
        this.smartIOT = smartIOT;
    }

    public override string ToString()
    {
        return $"Sensor: {Name}, Value: {Value}";
    }
}

public class SensorCollection : Collection<Sensor>
{
    SmartIOT smartIOT;

    public SensorCollection(SmartIOT smartIOT)
    {
        this.smartIOT = smartIOT;
    }

    protected override void InsertItem(int index, Sensor item)
    {
        base.InsertItem(index, item);
        smartIOT.UpdateSensorData(item);
    }
    protected override void SetItem(int index, Sensor item)
    {
        base.SetItem(index, item);
        smartIOT.UpdateSensorData(item);
    }
    protected override void RemoveItem(int index)
    {
        this[index].smartIOT = null;
        base.RemoveItem(index);
    }
    protected override void ClearItems()
    {
        foreach (Sensor sensor in Items)
        {
            sensor.smartIOT = null;
        }
        base.ClearItems();
    }
}

public class SmartIOT
{
    public readonly SensorCollection Sensors;
    public SmartIOT()
    {
        Sensors = new SensorCollection(this);
    }
    public void UpdateSensorData(Sensor sensor)
    {
        Console.WriteLine($"Updating data for sensor: {sensor.Name}, New Value: {sensor.Value}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        SmartIOT smartIOT = new SmartIOT();

        Sensor sensor1 = new Sensor("Temperature", 22.5, smartIOT);
        Sensor sensor2 = new Sensor("Humidity", 60.0, smartIOT);
        Sensor sensor3 = new Sensor("Pressure", 1013.25, smartIOT);

        Console.WriteLine("Adding sensors to SmartIOT...");
        smartIOT.Sensors.Add(sensor1);
        smartIOT.Sensors.Add(sensor2);
        smartIOT.Sensors.Add(sensor3);

        sensor2.Value = 65.0; 
        smartIOT.Sensors[1] = sensor2; 

        Console.WriteLine("\nRemoving the temperature sensor...");
        smartIOT.Sensors.RemoveAt(0); 

        Console.WriteLine("\nClearing all sensors...");

        Console.WriteLine("\nRemaining Sensors:");
        foreach (var sensor in smartIOT.Sensors)
        {
            Console.WriteLine(sensor);
        }
    }
}