using Godot;
using System;

public partial class Player : Node3D
{
    private const float RayLength = 1000.0f;


    public override void _PhysicsProcess(double delta)
    {
        var mousePos = Mouse3DPosition();
        if(mousePos != Vector3.Zero)
        {
            mousePos.Y = 1.5f;
            LookAt(mousePos);   
        }
    }

    public Vector3 Mouse3DPosition()
    {
        var mousePos = GetViewport().GetMousePosition();
        var camera = GetTree().Root.GetCamera3D();
        var origin = camera.ProjectRayOrigin(mousePos);
        var end = origin + camera.ProjectRayNormal(mousePos) * camera.Far;
        var query = PhysicsRayQueryParameters3D.Create(origin, end);
        var rayArr = GetWorld3D().DirectSpaceState.IntersectRay(query);

        if (rayArr.ContainsKey("position"))
        {
            return (Vector3)rayArr["position"];
        }

        return Vector3.Zero;
    }
}
