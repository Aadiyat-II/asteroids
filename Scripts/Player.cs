using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float RotationSpeed {get; set;} = 1.5f;

    [Export]
    public float Acceleration {get; set;} = 100.0f;

    [Export]
    public float MaxVelocity {get; set;} = 500.0f;

    private float GetAngleToMouse()
    {
        return (-Transform.Y).AngleTo(GetGlobalMousePosition() - Position);
    }

    private void Accelerate(double delta)
    {
        Velocity += (-Transform.Y) * Acceleration * (float)delta;
    }

    public override void _PhysicsProcess(double delta)
    {
        Rotation += RotationSpeed*Mathf.Sign(GetAngleToMouse())*(float)delta;

        if (Input.IsActionPressed("move"))
        {
            Accelerate(delta);
        }

        MoveAndSlide();
    }


}
