using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string ordinaryString = "Halo";
        ordinaryString += " ";
        ordinaryString += "Dunia";
        Console.WriteLine(ordinaryString);

        StringBuilder stringBuilder = new StringBuilder("Halo");
        stringBuilder.Append(" ");
        stringBuilder.Append("Dunia");
        Console.WriteLine(stringBuilder.ToString());
    }

    
}