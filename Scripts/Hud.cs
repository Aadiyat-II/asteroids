using Godot;
using System;

public partial class Hud : CanvasLayer
{
    private Label _scoreLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("ScoreLabel");
    }

    public void UpdateScore(int score)
    {
        _scoreLabel.Text = score.ToString();
    }


}
