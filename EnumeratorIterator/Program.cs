using System;
using System.Collections.Generic;

class Program
{
    public static IEnumerable<Operations> AllOperations()
    {
        foreach (Operations opr in Enum.GetValues(typeof(Operations)))
        {
            yield return opr;
        }
    }

    public enum Operations
    {
        Add,
        Substract,
        Multiply,
        Divide,
    }

    static void Main(string[] args)
    {
        foreach (var opr in AllOperations())
        {
            Console.WriteLine(opr);
        }
    }
}
