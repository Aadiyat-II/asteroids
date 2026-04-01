using Godot;

public partial class World : Node2D
{
    [Export]
    public PackedScene AsteroidScene {get; set;}

    private PathFollow2D _enemySpawnLocation;

    public override void _Ready()
    {
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
    }

    public void OnPlayerBulletFired(Bullet bullet)
    {
        AddChild(bullet);
    }

    private void OnAsteroidTimerTimeout()
    {
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        Asteroid asteroid = AsteroidScene.Instantiate<Asteroid>();

        // Set Position
        _enemySpawnLocation.ProgressRatio = GD.Randf();
        asteroid.Position = _enemySpawnLocation.Position;

        // Set Rotation
        var  direction = _enemySpawnLocation.Rotation + (Mathf.Pi / 2);
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        asteroid.Rotation = direction;

        // Asteroid sets its own velocity
        AddChild(asteroid);
    }
}
