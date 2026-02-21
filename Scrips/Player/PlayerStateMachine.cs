using Godot;
using System;

public partial class PlayerStateMachine : Node
{
    [Export] private State currentState;
    private Godot.Collections.Dictionary<string, State> states = [];

    public async override void _Ready()
    {
        base._Ready();

        FindAndProcessChildStates();
        await ToSignal(Owner, "ready");
        currentState.Enter(null);
    }

    private void FindAndProcessChildStates()
    {
        foreach (Node child in GetChildren())
        {
            if (child is State state)
            {
                states[child.Name] = state;
                state.stateName = child.Name;
                ConnectStateTransitionSignal(state);
            }
            else
                GD.PushWarning("State machine contains incompatible child node");
        }
    }

    private void ConnectStateTransitionSignal(State state)
    {
        State transitionSignal = state;
        transitionSignal.Transition += OnChildTransition;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        currentState.Update(delta);
    }

    public void OnChildTransition(string newStateName)
    {
        if (states.ContainsKey(newStateName))
        {
            State newState = states[newStateName];
            if (newState != currentState)
                LoadNewState(newState);
        }
        else
        {
            GD.PushWarning("State does not exist");
        }
    }

    private void LoadNewState(State newState)
    {
        currentState.Exit();
        newState.Enter(currentState);
        currentState = newState;
    }
    
}
