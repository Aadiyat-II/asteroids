using Godot;
using System;

[GlobalClass]
public partial class AsteroidStats : Resource
{
    [Export]
    public int maxSpeed { get; set;}
    [Export]
    public int minSpeed { get; set;}
    [Export]
    public int hitPoints {get; set;}
    [Export]
    public Texture2D SpriteTexture {get; set;}
    [Export]
    public Shape2D CollisionShape { get; set;}
    [Export]
    public AsteroidType asteroidType {get; set;}
}
