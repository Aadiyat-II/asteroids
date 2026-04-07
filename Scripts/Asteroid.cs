using Godot;
using System;

public partial class Asteroid : RigidBody2D, Targetable
{
    [Signal]
    public delegate void AsteroidExplodedEventHandler(Asteroid[] asteroids);

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
            _collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            Explode();
            QueueFree();
        }
    }

    private void Explode()
    {
        int numChildren = 2;
        var asteroidScene = GD.Load<PackedScene>(SceneFilePath);

        if(asteroidStats.asteroidType == AsteroidType.SMALL) return;

        AsteroidStats childStats = asteroidStats.asteroidType switch
        {
            AsteroidType.LARGE => GD.Load<AsteroidStats>("res://Resources/asteroid_medium_stats.tres"),
            AsteroidType.MEDIUM => GD.Load<AsteroidStats>("res://Resources/asteroid_small_stats.tres"),
            _ => throw new ArgumentOutOfRangeException(),
        };

        Asteroid[] children = new Asteroid[numChildren];
        for(int i = 0; i < numChildren; i++)
        {
            Asteroid asteroidChild = asteroidScene.Instantiate<Asteroid>();
            asteroidChild.Position = Position;
            asteroidChild.Rotation =  (float)GD.RandRange(0, 2*Mathf.Pi);
            asteroidChild.asteroidStats = childStats;
            children[i] = asteroidChild;
            
        }
        
        EmitSignal(SignalName.AsteroidExploded, children);
    }
       
}
