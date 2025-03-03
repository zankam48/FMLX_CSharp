using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "data.txt";

        try
        {
            string file = File.ReadAllText(filePath);   
        }
        catch (FileNotFoundException exc1)
        {
            Console.WriteLine($"File {filePath} not found.");
            Console.WriteLine($"Excepetion : {exc1.Message}");
        }
        finally
        {
            Console.WriteLine("Program finished.");
        }
    }
}