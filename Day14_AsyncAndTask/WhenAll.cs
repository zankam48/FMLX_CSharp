using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task<int> evenCountTask = GetEvensCountAsync(2, 100);
        Task<int> oddCountTask = GetOddsCountAsync(2, 100);

        await Task.WhenAll(evenCountTask, oddCountTask);
        Console.WriteLine($"Even count: {evenCountTask.Result}");
        Console.WriteLine($"Odd count: {oddCountTask.Result}");

    }

    static async Task<int> GetEvensCountAsync(int start, int end)
    {
        return await Task.Run(() => 
        {
            return Enumerable.Range(start, end).Count(n => n%2 == 0);
        });
    }

    static async Task<int> GetOddsCountAsync(int start, int end)
    {
        return await Task.Run(() => 
        {
            return Enumerable.Range(start, end).Count(n => n%2 != 0);
        });
    }
}