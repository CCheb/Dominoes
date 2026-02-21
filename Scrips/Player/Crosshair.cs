using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Crosshair : Control
{
	[Export] public float Radius = 20f;
    [Export] public float Thickness = 2f;
    [Export] public Color CrosshairColor = Colors.White;
    [Export] public int Segments = 64;

    private bool increment;
    public override void _Ready()
    {
        SetAnchorsPreset(LayoutPreset.Center);
        Position = GetViewportRect().Size / 2f;
    }

    public override void _Draw()
    {
        DrawArc(
            Vector2.Zero,   // Center relative to this Control
            Radius,
            0,
            Mathf.Tau,
            Segments,
            CrosshairColor,
            Thickness
        );
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Radius >= 80)
            increment = false;
        else if(Radius <= 20)
            increment = true;
        

        if(increment)
            Radius += 1;
        else
            Radius -= 1;

        QueueRedraw();
    }
}
