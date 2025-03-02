using System;

public class MathOperations
{
    // readonly
    public readonly double Pi;

    // writeonly - setter only
    private double _operand1;
    public double Operand1
    {
        set
        {
            if (value < 0)
                throw new ArgumentException("Operand must be non-negative.");
            _operand1 = value;
        }
    }

    // automatic property -> no field
    public double Operand2 { get; set; }

    // static constructor - dieksekusi sekali ketika class dijalankan, fungsinya buat ngurangin redundancy, kl ada multiple constructor dan pen implementasi suatu code yg sama bisa pake static constructor
    static MathOperations()
    {
        Console.WriteLine("Static constructor called - Initializing MathOperations class.");
    }

    // default constructor
    public MathOperations()
    {
        Pi = 3.14159; 
    }

    // constructor with params
    public MathOperations(double operand1, double operand2)
    {
        Pi = 3.14159; 
        _operand1 = operand1;
        Operand2 = operand2;
        Console.WriteLine("Constructor with parameters called.");
    }

    public double CalculateCircleArea()
    {
        return Pi * _operand1 * _operand1;
    }

    public double AddOperands()
    {
        return _operand1 + Operand2;
    }

    public double MultiplyOperands()
    {
        return _operand1 * Operand2;
    }

    public double GetOperand1()
    {
        return _operand1;
    }
}