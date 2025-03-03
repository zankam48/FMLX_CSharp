using System;
using System.Collections.Generic;

class Program
{
    public static IEnumerable<int> FilterNumbers()
    {
        for (int i=0; i<=5; i++)
        {
            if (i % 2 == 0)
            {
                yield return i*2;
            }
        }

        for (int i=10; i<=15; i++)
        {
            if (i%3 == 0)
            {
                yield return i*3;
            }
        } 
    }
    static void Main(string[] args)
    {
        foreach (var number in FilterNumbers())
        {
            Console.WriteLine(number);
        }
    }
}