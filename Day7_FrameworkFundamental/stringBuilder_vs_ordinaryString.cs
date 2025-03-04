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

        // performance comparison
        int iterations = 100000;
        DateTime start = DateTime.Now;
        string testString = "";
        for (int i=0; i< iterations; i++){
            testString += "hi";
        }
        DateTime end = DateTime.Now;
        Console.WriteLine("ordinary string : " + (end-start).TotalMilliseconds + "ms");

        start = DateTime.Now;
        StringBuilder testStringBuilder = new StringBuilder();
        for (int i=0; i<iterations; i++){
            testStringBuilder.Append("hi");
        }
        end = DateTime.Now;
        Console.WriteLine("string builder : " + (end-start).TotalMilliseconds + "ms");

    }


}