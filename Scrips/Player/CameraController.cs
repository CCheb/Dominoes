using Godot;
using System;

public partial class CameraController : Node3D
{
	private float time = 0.0f;
	private Vector3 basePosition;
	private float currentFov;
	private float targetFov;
	private Camera3D camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera = GetNode<Camera3D>("Camera3D");
		currentFov = camera.Fov;
		targetFov = currentFov;
	}

	public void RequestCameraZoom(float desiredFov)
	{
		if(desiredFov >= 30.0f && desiredFov <= 75.0f)
			targetFov = desiredFov;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentFov = Mathf.Lerp(currentFov, targetFov, 7.0f*(float)delta);
		camera.Fov = currentFov;
	}
}
