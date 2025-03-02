class Program
{
    static void Main(string[] args)
    {
        // using default constructor
        MathOperations math1 = new MathOperations();
        Console.WriteLine($"Area of Circle (radius 3): {math1.CalculateCircleArea()}");

        // using constructor with params
        MathOperations math2 = new MathOperations(3, 4);
        Console.WriteLine($"Sum of Operands: {math2.AddOperands()}");
        Console.WriteLine($"Product of Operands: {math2.MultiplyOperands()}");

        // writeonly
        math2.Operand1 = 5; 

        Console.WriteLine($"Updated Operand1: {math2.GetOperand1()}");
    }
}