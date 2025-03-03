using System;

public delegate void TemperatureChangedHandler(double temprature);
public delegate void HumidityChangedHandler(double humidity);

public class Publisher
{
    // declare an event
    public event TemperatureChangedHandler TemperatureChanged;
    public event HumidityChangedHandler HumidityChanged;


    // method to trigger the event
    public void ChangeTemperature(double newTemperature)
    {
        TemperatureChanged?.Invoke(newTemperature);
    }

    public void ChangeHumidity(double newHumidity)
    {
        HumidityChanged?.Invoke(newHumidity);
    }
}

public class Subsriber
{
    public void onTemperatureChanged(double newTemperature)
    {
        Console.WriteLine($"Temperature changed to {newTemperature}");
    }
    public void onHumidityChanged(double newHumidity)
    {
        Console.WriteLine($"Humidity changed to {newHumidity}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Publisher publisher = new Publisher();
        Subsriber subsriber = new Subsriber();

        publisher.TemperatureChanged += subsriber.onTemperatureChanged;
        publisher.HumidityChanged += subsriber.onHumidityChanged;

        publisher.ChangeTemperature(25.5);
        publisher.ChangeHumidity(60.0);

        publisher.TemperatureChanged -= subsriber.onTemperatureChanged;
    } 
}