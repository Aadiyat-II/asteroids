using Godot;

public partial class World : Node2D
{
    [Export]
    public PackedScene AsteroidScene {get; set;}

    private PathFollow2D _enemySpawnLocation;

    private int _score;
    private Hud _hud;

    public override void _Ready()
    {
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
        _hud = GetNode<Hud>("HUD");
    }

    public void OnPlayerBulletFired(Bullet bullet)
    {
        AddChild(bullet);
    }

    public void OnAsteroidExploded(Asteroid[] asteroids, int score)
    {
        _score += score;
        _hud.UpdateScore(_score);

        foreach(var asteroid in asteroids)
        {
            asteroid.AsteroidExploded += OnAsteroidExploded;
            CallDeferred(MethodName.AddChild, asteroid);
        }
        GD.Print(_score);
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
        asteroid.AsteroidExploded += OnAsteroidExploded;
        AddChild(asteroid);
    }
}
