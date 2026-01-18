using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float RotationSpeed {get; set;} = 1.0f;

    [Export]
    public float Acceleration {get; set;} = 2.0f;

    [Export]
    public float MaxVelocity {get; set;} = 10.0f;

    private float GetAngleToMouse()
    {
        return Vector2.FromAngle(Rotation - Mathf.Pi/2).AngleTo(GetGlobalMousePosition());
    }

    public override void _PhysicsProcess(double delta)
    {
        Rotation += RotationSpeed*Mathf.Sign(GetAngleToMouse())*(float)delta;
    }


}
