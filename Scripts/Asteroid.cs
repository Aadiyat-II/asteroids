using Godot;
using System;

public partial class Asteroid : RigidBody2D
{
    [Export]
    public float maxSpeed {get; set;}
    [Export]
    public float minSpeed {get; set;}

    private Vector2 movementVector = new(1, 0);
    public override void _Ready()
    {
        GD.Print(Rotation);
        LinearVelocity = movementVector.Rotated(Rotation) * (float)GD.RandRange(minSpeed, maxSpeed);
        GD.Print("Travelling at:");
        GD.Print(LinearVelocity);
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    }
}
