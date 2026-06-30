using Godot;

public partial class World : Node2D
{
    [Export]
    public PackedScene AsteroidScene {get; set;}

    [Export]
    public int InitialLives {get; set; }  = 3;

    private PathFollow2D _enemySpawnLocation;
    private Hud _hud; 

    private int _score = 0;
    private int _currLives;

    public override void _Ready()
    {
        _currLives = InitialLives;
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
        _hud = GetNode<Hud>("HUD");
        _hud.SetInitialLives(InitialLives);
    }

    public void OnPlayerBulletFired(Bullet bullet)
    {
        AddChild(bullet);
    }

    public void OnAsteroidExploded(Vector2 position, AsteroidStats variant, int score)
    {
        int numChildren = 2;
        _score += score;
        _hud.UpdateScore(_score);

        Asteroid[] children = new Asteroid[numChildren];
        for(int i = 0; i < numChildren; i++)
        {
            Asteroid asteroidChild = AsteroidScene.Instantiate<Asteroid>();
            asteroidChild.Position = position;
            asteroidChild.Rotation =  (float)GD.RandRange(0, 2*Mathf.Pi);
            asteroidChild.asteroidStats = variant;
            children[i] = asteroidChild;
            asteroidChild.AsteroidExploded += OnAsteroidExploded;
            CallDeferred(MethodName.AddChild, asteroidChild);
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
