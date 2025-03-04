using System;

public class Game
{
    private bool _end;

    private bool End
    {
        get {return this._end;}
        set {this._end = value;}
    }
    private Stack<State> states; 

    private void InitialStates()
    {
        this.states = new Stack<State>();
        // Console.WriteLine(states.GetHashCode());
        this.states.Push(new StateMainMenu(this.states));
    }
    
    private void InitialVar()
    {
        this._end = false; // this means the var belong to class not local
    }
    public Game()
    {
        this.InitialVar();
        this.InitialStates();
        Console.WriteLine("Game Class");
    }
    public void Run()
    {
        while (this._end == false)
        {
            Console.WriteLine("Running...");
            string? toEnd = Console.ReadLine();

            if (toEnd == "end")
            {
                this._end = true;
            } else 
            {
                Console.WriteLine("Invalid input. Please enter 'end' to stop.");
            }

            Console.WriteLine("Game End");
        }
    }
}