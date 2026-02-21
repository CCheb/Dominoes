using Godot;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

public partial class ShootingState : State
{
    [Export] private CameraController cameraController;
    [Export] private Crosshair crosshair;
    [Export] private AudioStreamPlayer3D gunSound;
    [Export] private PackedScene testDecal;
    public override async void Enter(State prevState)
    {
        // If prevState == Shooting: reset back to idle here

        GD.Print("Entered Shooting State");
        crosshair.RequestStop();
        Fire();
        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        EmitSignalTransition("IdleState");
    }

    private void Fire()
    {
        // Find out if the ray intersected with a body. It will return nothing if not
        Godot.Collections.Dictionary collisionResult = CalculateRay();

        if(collisionResult.Count != 0)
            SpawnDecal((Vector3)collisionResult["position"]);

        // TODO: Play firing animations/sounds here
        gunSound.Play();
        
    }

    private Godot.Collections.Dictionary CalculateRay()
    {
        Camera3D camera = cameraController.GetCamera();

        // Grab the worlds 3D physics state/sandbox. This state is where all of the physics occurs and its handled by the physics server
		var spaceState = camera.GetWorld3D().DirectSpaceState;

        // Circular instead of square
        float angle = (float)GD.RandRange(0, Mathf.Tau);
        float radius = (float)GD.RandRange(0, crosshair.Radius);

        Vector2 randomOffset = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle)) * radius;
        Vector2 screenPoint = crosshair.Position + randomOffset;    // In Screen space

        // In world space
        Vector3 originPoint = camera.ProjectRayOrigin(screenPoint);
        Vector3 direction = camera.ProjectRayNormal(screenPoint);
        Vector3 endPoint = originPoint + direction * 1000f;
    
		var query = PhysicsRayQueryParameters3D.Create(originPoint, endPoint);
		query.CollideWithBodies = true;
		query.CollideWithAreas = true;
		query.CollisionMask = (1 << 0) | (1 << 1) | (1 << 2); // Detect layers 1, 2, and 3
		
		// We are essentially creating a dictionary holding a number of keys that pertain to the collision information
		var collisionResult = spaceState.IntersectRay(query);
        return collisionResult;
    }

    private void SpawnDecal(Vector3 position)
    {
        Node3D decal = testDecal.Instantiate<Node3D>();
        GetTree().Root.AddChild(decal);
        decal.Position = position;
    }

    public override void Exit()
    {
        return;
    }

    public override void Update(double delta)
    {
        return;
    }


}
