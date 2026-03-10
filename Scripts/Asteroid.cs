using Godot;
using System;

public partial class Asteroid : RigidBody2D, Targetable
{
    [Export]
    public float maxSpeed {get; set;}
    [Export]
    public float minSpeed {get; set;}

    [Export]
    public int hitPoints {get; set;} = 3;

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

    public void OnHit()
    {
        hitPoints -= 1;
        if(hitPoints <= 0)
        {
            QueueFree();
        }
    }
       
}
