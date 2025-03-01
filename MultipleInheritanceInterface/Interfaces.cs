using System;

public interface ISensor
{
    void ReadValue();
    string SensorInfo();
}

public interface IActuator
{
    void Activate();
    void Deactivate();
}