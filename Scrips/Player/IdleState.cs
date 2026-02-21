using Godot;
using System;

public partial class IdleState : State
{
    [Export] private CameraController cameraController;
    public override void Enter(State prevState)
    {
        // If prevState == Shooting: reset back to idle here
        GD.Print("Entered Idle State!");
        cameraController.RequestCameraZoom(75.0f);
    }

    public override void Exit()
    {
        return;
    }

    public override void Update(double delta)
    {
        if(Input.IsActionPressed("aim"))
            EmitSignalTransition("AimingState");
    }
}
