// StreamWriter writes text data as individual characters

using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        StreamWriter sw = new StreamWriter("");
        Console.WriteLine("Enter the text ");
        string str = Console.ReadLine();
        sw.Write(str);
        sw.Flush();
        sw.Close();
        Console.ReadKey();
    }
}