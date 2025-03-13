using System;
using System.Threading;

public class ThreadLifecycleExample
{
    public static void Main(string[] args)
    {
        Thread myThread = new Thread(SleepAndPrint);

        Console.WriteLine("Thread created. IsAlive: " + myThread.IsAlive);

        myThread.Start();

        Thread.Sleep(100); 
        Console.WriteLine("Thread started. IsAlive: " + myThread.IsAlive);

        myThread.Join(); 

        Console.WriteLine("Thread finished. IsAlive: " + myThread.IsAlive);

        Console.WriteLine("Main thread finished.");
    }

    public static void SleepAndPrint()
    {
        Console.WriteLine("Thread started sleeping...");
        Thread.Sleep(3000);
        Console.WriteLine("Thread finished sleeping.");
    }
}