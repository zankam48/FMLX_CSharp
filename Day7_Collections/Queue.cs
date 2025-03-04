using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Queue<int> q = new Queue<int>();
        q.Enqueue(1);
        q.Enqueue(2);

        Console.WriteLine("Dequeued element: " + q.Dequeue());
    }
}