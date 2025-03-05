using System;
using System.Threading;

public class Board
{
    private char[] verticalSquare;
    private int currentPosition;

    public Board(int size)
    {
        // Initialize the vertical square with underscores
        verticalSquare = new char[size];
        for (int i = 0; i < size; i++)
        {
            verticalSquare[i] = '_';
        }

        // Initially place 'X' at the first position
        verticalSquare[0] = 'X';
        currentPosition = 0;
    }

    // Method to display the vertical square on the console
    public void DrawBoard()
    {
        Console.Clear();
        for (int i = 0; i < verticalSquare.Length; i++)
        {
            Console.WriteLine($"[{verticalSquare[i]}]");
        }
    }

    // Method to handle moving 'X' based on the input jump distance
    public void MoveX(int jump)
    {
        // Calculate the new position of 'X'
        int newPosition = currentPosition + jump;

        // Check if the new position is within bounds
        if (newPosition < verticalSquare.Length)
        {
            // Reset the current position and move 'X' to the new position
            verticalSquare[currentPosition] = '_';
            currentPosition = newPosition;
            verticalSquare[currentPosition] = 'X';
        }
        else
        {
            Console.WriteLine("Jump exceeds the size of the list. Try a smaller number.");
            Thread.Sleep(1000); // Wait a bit before clearing the message
        }
    }

    // Method to check if the game is over (when 'X' reaches the last position)
    public bool IsGameOver()
    {
        return currentPosition == verticalSquare.Length - 1;
    }

    // Method to get the current position of 'X'
    public int GetCurrentPosition()
    {
        return currentPosition;
    }
}
