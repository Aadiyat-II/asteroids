using Godot;
using System;

public partial class Asteroid : RigidBody2D, Targetable
{
    [Signal]
    public delegate void AsteroidExplodedEventHandler(Vector2 position, AsteroidStats variant, int score);

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

        LinearVelocity = movementVector.Rotated(Rotation) * (float)GD.RandRange(asteroidStats.MinSpeed, asteroidStats.MaxSpeed);
        GD.Print("Travelling at:");
        GD.Print(LinearVelocity);
    }

    private void ApplyVariant()
    {
        _sprite.Texture = asteroidStats.SpriteTexture;
        _collisionShape.Shape = asteroidStats.CollisionShape;
        _hitPoints = asteroidStats.HitPoints;
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
            _collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

            Explode();
            QueueFree();
        }
    }

    private void Explode()
    {
        if(asteroidStats.nextVariant == null) return;
        EmitSignal(SignalName.AsteroidExploded, Position, asteroidStats.nextVariant, asteroidStats.ScoreValue);
    }
       
}
