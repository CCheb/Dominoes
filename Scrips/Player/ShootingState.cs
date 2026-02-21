using Godot;
using System;

public partial class ShootingState : State
{
     public override void Enter(State prevState)
    {
        // If prevState == Shooting: reset back to idle here
        GD.Print("Entered Shooting State");
    }

    public override void Exit()
    {
        return;
    }

    public override void Update(double delta)
    {
        return;
    }
}
