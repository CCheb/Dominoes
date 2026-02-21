using Godot;
using System;

public partial class StartScreen : Node3D
{
    [Export] Button start;
    [Export] Button help;
    [Export] Button secret;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/main_level.tscn");
    }

    public void _on_help_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/help_screen.tscn");
    }

    public void _on_secret_pressed()
    {
        GetTree().ChangeSceneToFile("res://Do not look here/Nothing to see here/IM WARNING YOU/Alright, you asked for this/secret_scene.tscn");
    }
}
