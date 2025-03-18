using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string file = "";
        int a = 2;
        int b = 2;
        result = a + b;

        using (StreamWriter writer = new StreamWriter(file))
        {
            writer.Write($"sum of {a} + {b} = {result}");
        }
        Console.WriteLine("saved");
        Console.ReadKey();
    }
}