using Godot;
using System;

public partial class Asteroid : RigidBody2D, Targetable
{
    [Export]
    public AsteroidStats asteroidStats {get; set;}
    private Vector2 movementVector = new(1, 0);
    private Sprite2D _sprite;
    private CollisionShape2D _collisionShape;
    private int _hitPoints;
    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        
        ApplyVariant();

        LinearVelocity = movementVector.Rotated(Rotation) * (float)GD.RandRange(asteroidStats.minSpeed, asteroidStats.maxSpeed);
        GD.Print("Travelling at:");
        GD.Print(LinearVelocity);
    }

    private void ApplyVariant()
    {
        _sprite.Texture = asteroidStats.SpriteTexture;
        _collisionShape.Shape = asteroidStats.CollisionShape;
        _hitPoints = asteroidStats.hitPoints;
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    }

    public void OnHit()
    {
        _hitPoints -= 1;
        if(_hitPoints <= 0)
        {
            QueueFree();
        }
    }
       
}
