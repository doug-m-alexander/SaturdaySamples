using System.Numerics;
using Raylib_cs;

class Particle
{
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Radius { get; set; }
  public Color Color { get; set; }
  public float Lifetime { get; set; }
  public float FadeoutTime { get; set; }
  public Vector2 Gravity { get; set; }

  public Particle(Vector2 position, Vector2 velocity, float radius, Color color, float lifetime, float fadeoutTime, Vector2 gravity)
  {
    Position = position;
    Velocity = velocity;
    Radius = radius;
    Color = color;
    Lifetime = lifetime;
    FadeoutTime = fadeoutTime;
    Gravity = gravity;
  }

  public void Update(float deltaTime)
  {
    Position += Velocity * deltaTime;
    Velocity += Gravity * deltaTime;
    Lifetime -= deltaTime;
  }

  public void Draw()
  {
    float alpha = Lifetime > FadeoutTime ? 1.0f : Lifetime / FadeoutTime;
    Color fadedColor = new Color(Color.r, Color.g, Color.b, (byte)(Color.a * alpha));

    Raylib.DrawCircleV(Position, Radius, fadedColor);
  }
}
