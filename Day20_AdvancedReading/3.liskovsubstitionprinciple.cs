public class Sensor
{
    private string type {get; set;}
    public void DiscreteOutput(bool output)
    {
        return output
    }
}

public class AccSensor : Sensor 
{}

public class IRSensor : Sensor
{}

// better to use
public interface IDigital
{
    public void DiscreteOutput();
}

public interface IAnalog
{
    public void ContinousOutput();
}

public class AccSensor : Sensor, IAnalog
{}

public class IRSensor : Sensor, IDigital
{}