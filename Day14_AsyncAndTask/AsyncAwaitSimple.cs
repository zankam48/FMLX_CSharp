using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Main started");

        await DoWorkAsync(); 

        Console.WriteLine("Main finished");
    }

    static async Task DoWorkAsync()
    {
        Console.WriteLine("Task started");
        await Task.Delay(2000);
        Console.WriteLine("Task completed");
    }
}

// VS

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Main started");

        Task workTask = DoWorkAsync();

        Console.WriteLine("Main finished");

        await workTask;
    }

    static async Task DoWorkAsync()
    {
        Console.WriteLine("Task started");
        await Task.Delay(2000);
        Console.WriteLine("Task completed");
    }
}
