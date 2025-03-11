using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int size = 20;
        char[] verticalSquare = new char[size];

        for (int i = 0; i < size; i++)
        {
            verticalSquare[i] = '_';
        }

        verticalSquare[0] = 'X';

        int currentPosition = 0;

        while (currentPosition < size - 1)
        {
            Console.Clear();
            DrawVerticalSquare(verticalSquare);

            Console.Write("Enter the jump distance: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int jump))
            {
                int newPosition = currentPosition + jump;

                if (newPosition < size)
                {
                    verticalSquare[currentPosition] = '_';

                    currentPosition = newPosition;
                    verticalSquare[currentPosition] = 'X';
                }
                else
                {
                    Console.WriteLine("Jump exceeds the size of the list. Try a smaller number.");
                    Thread.Sleep(1000); 
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
                Thread.Sleep(1000); 
            }
        }
        Console.Clear();
        DrawVerticalSquare(verticalSquare);
        Console.WriteLine("The 'X' has reached the last position!");
    }

    static void DrawVerticalSquare(char[] square)
    {
        for (int i = 0; i < square.Length; i++)
        {
            Console.WriteLine($"[{square[i]}]");
        }
    }
}
