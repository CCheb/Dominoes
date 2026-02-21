using Godot;
using System;

public partial class SecretScene : Node3D
{
    [Export] public Button back;


    public override void _Ready()
    {
        base._Ready();
    }

    public void _on_back_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/start_screen.tscn");
    }

}
