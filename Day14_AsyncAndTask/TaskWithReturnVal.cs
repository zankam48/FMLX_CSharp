using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task<char> task1 = Task.Run(() => GetFirstCharacter("Hello"));

        string data = "halo dunia";
        Task<char> task2 = Task.Run(() => GetFirstCharacter(data));

        Task<char> task3 = Task.Run(() =>
        {
            string someString = "contoh";
            return someString[0];
        });

        Task<char> task4 = Task.Run((string input) => input[0], "halo");

        task1.ContinueWith(t => Console.WriteLine($"Task 1 result: {t.Result}"));
        task2.ContinueWith(t => Console.WriteLine($"Task 2 result: {t.Result}"));
        task3.ContinueWith(t => Console.WriteLine($"Task 3 result: {t.Result}"));
        task4.ContinueWith(t => Console.WriteLine($"Task 4 result: {t.Result}"));

        Console.ReadKey();
    }

    static char GetFirstCharacter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Gak bisa null/empty ygy");
        }
        return input[0];
    }
}