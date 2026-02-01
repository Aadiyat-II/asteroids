using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void BulletFiredEventHandler(Bullet bullet);

    [Export]
    public float RotationSpeed {get; set;} = 1.5f;

    [Export]
    public float Acceleration {get; set;} = 50.0f;

    [Export]
    public float MaxVelocity {get; set;} = 250.0f;

    [Export]
    public PackedScene BulletScene {get; set;}

    private AnimatedSprite2D _animatedSprite;

    private Marker2D _muzzle;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _muzzle = GetNode<Marker2D>("Muzzle");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("fire"))
        {
            FireBullet();
        }
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

    private void FireBullet()
    {   
        Bullet bullet = BulletScene.Instantiate<Bullet>();
        bullet.Position = _muzzle.GlobalPosition;
        bullet.Rotation = Rotation;
        EmitSignal("BulletFired", bullet);
    }

}
