using Godot;
using System;

[GlobalClass]
public partial class AsteroidStats : Resource
{
    [Export]
    public int MaxSpeed { get; set;}
    [Export]
    public int MinSpeed { get; set;}
    [Export]
    public int HitPoints {get; set;}
    [Export]
    public int ScoreValue { get; set; }
    [Export]
    public Texture2D SpriteTexture {get; set;}
    [Export]
    public Shape2D CollisionShape { get; set;}
    [Export]
    public AsteroidStats nextVariant { get; set; }
}
