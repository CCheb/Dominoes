using Godot;
using System;

public partial class TestDecal : Node3D
{
	public async override void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(3.0f), "timeout");
		QueueFree();
	}
}
