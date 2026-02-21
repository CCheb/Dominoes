using Godot;
using System;

public partial class SecretScene : Node3D
{
    [Export] public AudioStreamPlayer2D audio;
    [Export] public Button back;


    public override void _Ready()
    {
        base._Ready();

        audio.Play();
    }

    public void _on_back_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/start_screen.tscn");
    }

}
