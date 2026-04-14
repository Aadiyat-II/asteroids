using Godot;
using System;

public partial class Hud : CanvasLayer
{
    private Label _scoreLabel;
    private HBoxContainer _healthContainer;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _healthContainer = GetNode<HBoxContainer>("HealthContainer");
    }

    public void SetInitialLives(int lives)
    {
         // Make as many heart icons as max health
        for(int i = 0; i < lives; i++)
        {
            TextureRect rect = new TextureRect
            {
                Texture = GD.Load<Texture2D>("res://Assets/player_idle.png")
            };

            _healthContainer.AddChild(rect);
        }
    }

    public void UpdateScore(int score)
    {
        _scoreLabel.Text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        var children = _healthContainer.GetChildren();
        var currLifeIcon = -1;

        for(int i = 0; i < children.Count; i++)
        {
            if(children[i] is TextureRect lifeIcon)
            {
                if(++currLifeIcon >= lives)
                {
                    lifeIcon.Visible = false;
                }
            }
        }
    }
}
