using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Thread t = new Thread(printLetters);
        t.Start();

        // main thread
        for (int i=1; i<=10; i++)
        {
            Console.WriteLine(i);
        }

        
    }

    static void printLetters()
    {
        string letters = "abcdefghij";
        foreach (char c in letters)
        {
            Console.WriteLine(c);
        }
    }
}