using System;

public class Game
{
    private bool end
    {
        get {return this.end;}
        set {this.end = value;}
    }
    private Stack<State> states; 

    private void InitialStates()
    {
        this.states = new Stack<State>();
        // Console.WriteLine(states.GetHashCode());
        this.states.Push(new State(this.states));
    }
    
    private void InitialVar()
    {
        this.end = false; // this means the var belong to class not local
    }
    public Game()
    {
        this.InitialVar();
        this.InitialStates();
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