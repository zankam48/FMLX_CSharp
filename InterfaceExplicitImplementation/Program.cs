using IoTInterfaces;

class Program
{
    static void Main()
    {
        ISensor voltageSensor = new VoltageSensor();
        ISensor currentSensor = new CurrentSensor();
        IActuator relay = new Relay();
        PowerFactorCorrector pfc = new PowerFactorCorrector();

        voltageSensor.ReadValue();
        currentSensor.ReadValue(); 
        ((ISensor)pfc).ReadValue();

        relay.Activate();
        relay.Deactivate();      
        ((IActuator)pfc).Activate();          
        ((IActuator)pfc).Deactivate();
    }
}