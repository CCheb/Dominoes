using Godot;
using System;

public partial class HelpScreen : Node3D
{
    [Export] public Button back;

    public void _on_back_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/start_screen.tscn");
    }
}
