using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Crosshair : Control
{
    [ExportGroup("Crosshair Settings")]
	[Export] private float Radius = 20f;
    [Export] private float Thickness = 2f;
    [Export] private Color CrosshairColor = Colors.White;
    [Export] private int Segments = 64;
   
    private SinePatterns sinePattern;
    private float xOffset  = 0.0f;
    private float yValue = 0.0f;
    private Vector2 screenCenter;
    private Vector2 basePosition;
    private bool increment;
    public override void _Ready()
    {
        GD.Randomize();
        sinePattern = new();
        SetAnchorsPreset(LayoutPreset.Center);
        Position = GetViewportRect().Size / 2f;
        basePosition = Position;
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

    public void RequestReset()
    {
        sinePattern.SetCurrentPattern(SinePatterns.Patterns.RegularSine);
        Position = basePosition;
        yValue = xOffset = 0.0f;
        //direction = GD.Randf() < 0.5f ? -1 : 1;
        Visible = false;
    }

    public void RequestPatternLoad(SinePatterns.Patterns newPattern)
    {   
        sinePattern.SetCurrentPattern(newPattern);
        Visible = true;
    }

    public override void _Process(double delta)
    {

        base._Process(delta);

        if(!Visible)
            return;

        CrosshairGrowShrink();
        

        // Move Y back and forth
        sinePattern.ProcessCurrentPattern(ref yValue, ref xOffset, delta);

        Position = screenCenter + new Vector2(xOffset + basePosition.X, yValue + basePosition.Y);

        QueueRedraw();
    }

    private void CrosshairGrowShrink()
    {
        if(Radius >= 90)
            increment = false;
        else if(Radius <= 30)
            increment = true;
        

        if(increment)
            Radius += 1;
        else
            Radius -= 1;
    }


}
