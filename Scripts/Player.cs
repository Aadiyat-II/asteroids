using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float RotationSpeed {get; set;} = 1.5f;

    [Export]
    public float Acceleration {get; set;} = 50.0f;

    [Export]
    public float MaxVelocity {get; set;} = 250.0f;

     private AnimatedSprite2D _animatedSprite;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }


    public override void _PhysicsProcess(double delta)
    {
        FaceMouse(delta);

        if (Input.IsActionPressed("move"))
        {
            Accelerate(delta);
            _animatedSprite.Play("move");
        }
        else
        {
            _animatedSprite.Stop();
        }


        MoveAndSlide();
    }

    private void Accelerate(double delta)
    {
        Velocity += (-Transform.Y) * Acceleration * (float)delta;
    }

    private void FaceMouse(double delta)
    {
        Rotation += RotationSpeed * Mathf.Sign(GetAngleToMouse()) * (float)delta;
    }

    private float GetAngleToMouse()
    {
        return (-Transform.Y).AngleTo(GetGlobalMousePosition() - Position);
    }

}
