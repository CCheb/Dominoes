using Godot;
using System;

public partial class CamPivot : Node3D
{
    public float rotationSpeed = 20.0f;

    public override void _Process(double delta)
    {
        RotateY(Mathf.DegToRad(rotationSpeed * (float)delta));
    }
}
