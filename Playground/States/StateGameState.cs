using System;
using System.Collections.Generic;
class StateGame : State
{
    public StateGame(Stack<State> states): base(states)
    {
        Console.WriteLine("From Game State");
    }
}