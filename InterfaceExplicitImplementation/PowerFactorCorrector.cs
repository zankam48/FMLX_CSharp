using IoTInterfaces;

public class PowerFactorCorrector : ISensor, IActuator
{
    public double PowerFactor { get; set; }

    void ISensor.ReadValue()
    {
        Console.WriteLine("Reading power factor value...");
        PowerFactor = 0.95;
        Console.WriteLine($"Power Factor: {PowerFactor}");
    }

    void IActuator.Activate()
    {
        Console.WriteLine("Power Factor Corrector activated.");
    }

    void IActuator.Deactivate()
    {
        Console.WriteLine("Power Factor Corrector deactivated.");
    }

    string ISensor.Info()
    {
        return $"Power Factor Corrector: Power Factor = {PowerFactor}";
    }

    string IActuator.Info()
    {
        return $"Power Factor Corrector: Power Factor = {PowerFactor}";
    }
}