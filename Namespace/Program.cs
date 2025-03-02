using System;
using Sensors = RoboticComponents.Sensors;  
using Actuators = RoboticComponents.Actuators;  
using RoboticOperations;  

class Program
{
    static void Main(string[] args)
    {
        Sensors.Sensor temperatureSensor = new Sensors.Sensor("Temperature");
        Actuators.Actuator motorActuator = new Actuators.Actuator("Motor");

        Movement movement = new Movement(temperatureSensor, motorActuator);
        DoTask taskScheduler = new DoTask();

        taskScheduler.DoingTask("Move robot forward");
        movement.StartMovement();
    }
}
