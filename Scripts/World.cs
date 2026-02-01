using Godot;

public partial class World : Node2D
{
    public void OnPlayerBulletFired(Bullet bullet)
    {
        AddChild(bullet);
    }
}
