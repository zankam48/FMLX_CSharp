using System;

public class Game
{
    bool end;
    private void InitialVar()
    {
        this.end = false; // this means the var belong to class not local
    }
    public Game()
    {
        Console.WriteLine("Game Class");
    }
    public void Run()
    {
        while (this.end == false)
        {
            Console.WriteLine("Running...");
            string? toEnd = Console.ReadLine();

            if (toEnd == "end")
            {
                this.end = true;
            } else 
            {
                Console.WriteLine("Invalid input. Please enter 'end' to stop.");
            }

            Console.WriteLine("Game End");
        }
    }
}