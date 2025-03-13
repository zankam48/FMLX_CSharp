using System;
using System.Threading;
using System.Threading.Tasks;

public class ThreadPoolTaskExample
{
    public static void Main(string[] args)
    {
        
        ThreadPool.QueueUserWorkItem(Task1);
        ThreadPool.QueueUserWorkItem(Task2, "Hello from Task 2"); 
        ThreadPool.QueueUserWorkItem(Task3, 123); 

        Task.Run(() => AsyncMethod("Async task started"));

        Console.WriteLine("Main thread continues executing...");

        Thread.Sleep(2000);

        Console.WriteLine("Main thread finished.");
    }

    public static void Task1(object state)
    {
        Console.WriteLine("Task 1 executed on ThreadPool thread: " + Thread.CurrentThread.ManagedThreadId);
    }

    public static void Task2(object state)
    {
        string message = (string)state;
        Console.WriteLine($"Task 2 executed on ThreadPool thread: {Thread.CurrentThread.ManagedThreadId}, Message: {message}");
    }

    public static void Task3(object state)
    {
        int number = (int)state;
        Console.WriteLine($"Task 3 executed on ThreadPool thread: {Thread.CurrentThread.ManagedThreadId}, Number: {number}");
    }

    public static async Task AsyncMethod(string message)
    {
        Console.WriteLine($"{message} on Task.Run thread: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(1000);
        Console.WriteLine("Async task finished.");
    }
}