using Godot;
using System;

public abstract partial class State : Node
{
    [Signal] public delegate void TransitionEventHandler(string state);
    public abstract void Enter(State prevState);
    public abstract void Exit();
    public abstract void Update(double delta);
}
