using System;

namespace IoTInterfaces
{
    public interface ISensor
{
    void ReadValue();
    string Info();
}

public interface IActuator
{
    void Activate();
    void Deactivate();
    string Info();
}
}
