public class PowerFactorCorrector : ISensor, IActuator
{
    public double PowerFactor { get; set; }

    public void ReadValue()
    {
        Console.WriteLine("Reading power factor value...");
        PowerFactor = 0.95;
        Console.WriteLine($"Power Factor: {PowerFactor}");
    }

    public void Activate()
    {
        Console.WriteLine("Power Factor Corrector activated.");
    }

    public void Deactivate()
    {
        Console.WriteLine("Power Factor Corrector deactivated.");
    }

    public string Info()
    {
        return $"Power Factor Corrector: Power Factor = {PowerFactor}";
    }
}