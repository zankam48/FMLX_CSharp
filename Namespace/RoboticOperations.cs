namespace RoboticOperations
{
    using Sensors = RoboticComponents.Sensors;  // implementasi alias
    using Actuators = RoboticComponents.Actuators;  

    public class Movement
    {
        private Sensors.Sensor _sensor;
        private Actuators.Actuator _actuator;

        public Movement(Sensors.Sensor sensor, Actuators.Actuator actuator)
        {
            _sensor = sensor;
            _actuator = actuator;
        }

        public void StartMovement()
        {
            _sensor.ReadData();
            _actuator.Activate();
            Console.WriteLine("Movement started");
        }
    }

    public class DoTask
    {
        public void DoingTask(string task)
        {
            Console.WriteLine($"Doing Task: {task}");
        }
    }
}
