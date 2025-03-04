using System;
using System.Collections.Generic;

class StateMainMenu : State
{
    public StateMainMenu(Stack<State> states): base(states)
    {
        Console.WriteLine("State Main Menu");
    }
}