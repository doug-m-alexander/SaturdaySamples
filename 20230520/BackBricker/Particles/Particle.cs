using System.Numerics;
using Raylib_cs;

public class Particle
{
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public Color Color { get; set; }
  public float Radius { get; set; }
  public float Lifetime { get; set; }
}
