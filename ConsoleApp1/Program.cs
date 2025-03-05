using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int size = 10;
        char[] verticalSquare = new char[size];

        for (int i = 0; i < size; i++)
        {
            verticalSquare[i] = '_';
        }

        verticalSquare[0] = 'X';

        int counter = size;
        // int now = 0;
        while (counter > 1)
        {
            Console.WriteLine("Input a step : ");
            string step = Console.ReadLine();
            int intStep = Convert.ToInt32(step);
            for (int i = counter; i < intStep; i--)
            {
                Console.Clear();
                DrawVerticalSquare(verticalSquare);

                verticalSquare[counter] = '_';
                verticalSquare[counter + 1] = 'X'; 

                Thread.Sleep(500);
            }
            counter -= intStep; 
        }


        

        Console.Clear();
        // DrawVerticalSquare(verticalSquare);
    }

    static void DrawVerticalSquare(char[] square)
    {
        for (int i = 0; i < square.Length; i++)
        {
            Console.WriteLine($"[{square[i]}]");
        }
    }
}
