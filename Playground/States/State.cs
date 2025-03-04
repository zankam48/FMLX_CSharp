class State
{
    protected Stack<State> states;
    public State(Stack<State> states)
    {
        this.states = states;
        // Console.WriteLine(this.states.GetHashCode());
        // this.states.Push(new State(this.states));
    }
}