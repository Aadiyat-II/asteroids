using Godot;

public partial class Bullet : Area2D
{
    [Export]
    public float bulletSpeed = 800.0f;

    private Vector2 movementVector = new(0, -1);
    public override void _PhysicsProcess(double delta)
    {
        Position += movementVector.Rotated(Rotation) * bulletSpeed * (float)delta;
    }

    public void OnVisibleOnScreenNotifier2dScreenExited()
    {
        GD.Print("I'm Gone");
        QueueFree();
    }

    private void OnBodyEntered(Node2D body)
    {
        QueueFree();
        body.QueueFree();
    }
}
