using Godot;
using System;

public partial class AimingState : State
{
    [Export] private CameraController cameraController;
    [Export] private Crosshair crosshair;
    public override void Enter(State prevState)
    {
        // If prevState == Shooting: reset back to idle here
        GD.Print("Entered Aiming State");
        cameraController.RequestCameraZoom(10.0f);
        crosshair.RequestPatternLoad(SinePatterns.Patterns.DampedWave);
    }

    public override void Exit()
    {
        return;
    }

    public override void Update(double delta)
    {
        if(Input.IsActionJustReleased("aim"))
            EmitSignalTransition("ShootingState");
    }
}
