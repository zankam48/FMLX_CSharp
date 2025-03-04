using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Stack<int> s = new Stack<int>();
        s.Push(1);
        s.Push(2);

        Console.WriteLine("Dequeued element: " + s.Pop());
    }
}