using System;

class Program
{
    static void Main(string[] args)
    {
        string input = "123";
        int result;

        bool success = int.TryParse(input, out result);
        // bool success2 = 

        if (success)
        {
            Console.WriteLine("Converted integer: " + result);
        } else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }
    }
}