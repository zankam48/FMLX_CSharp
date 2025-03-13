using System;
using System.Threading;

public class ThreadSafetyExample
{
    private static int counter = 0;
    private static readonly object lockObject = new object(); // object for locking

    public static void Main(string[] args)
    {
        // without lock (race condition)
        Console.WriteLine("Without lock (race condition):");
        RunThreads(false);

        // with lock (thread safety)
        Console.WriteLine("\nWith lock (thread safety):");
        counter = 0; // reset counter
        RunThreads(true);
    }

    public static void RunThreads(bool useLock)
    {
        Thread thread1 = new Thread(() => IncrementCounter(useLock));
        Thread thread2 = new Thread(() => IncrementCounter(useLock));

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Final counter value: " + counter);
    }

    public static void IncrementCounter(bool useLock)
    {
        for (int i = 0; i < 1000; i++)
        {
            if (useLock)
            {
                lock (lockObject)
                {
                    counter++;
                }
            }
            else
            {
                counter++;
            }
        }
    }
}

// class myThread
// {
//     private static int counter = 0;
//     static void Main()
//     {
//         Thread thread1 = new Thread(Count);
//         thread1 thread2 = new Thread(Count);

//         thread1.Start();
//         if (thread1.IsAlive == true)
//         {
//             counter++;
//             thread1.Join();
//         }
//         thread2.Start();
//         if (thread2.IsAlive == true)
//         {
//             counter++;
//             thread2.Join();
//         }
//     }

//     static void Count()
//     {
//         Console.WriteLine()
//     }
// }

