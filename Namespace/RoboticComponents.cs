namespace RoboticComponents
{
    namespace Sensors
    {
        public class Sensor
        {
            public string SensorType { get; set; }
            
            public Sensor(string sensorType)
            {
                SensorType = sensorType;
            }

            public void ReadData()
            {
                Console.WriteLine($"Reading data from {SensorType}");
            }
        }
    }

    namespace Actuators
    {
        public class Actuator
        {
            public string ActuatorType { get; set; }

            public Actuator(string actuatorType)
            {
                ActuatorType = actuatorType;
            }

            public void Activate()
            {
                Console.WriteLine($"Activating {ActuatorType}");
            }
        }
    }
}
