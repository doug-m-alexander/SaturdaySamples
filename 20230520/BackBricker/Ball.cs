using Raylib_cs;
using System;
using System.Numerics;

public class Ball
{
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Radius { get; set; }
  public Color Color { get; set; }
  public Sound BounceSound { get; }
  public float Speed { get; }

  private static Random random = new Random();
  private Vector2 initialPosition;

  public Ball(Vector2 position, float radius, Color color, Sound bounceSound, float speed)
  {
    Position = position;
    initialPosition = position;
    Radius = radius;
    Color = color;
    BounceSound = bounceSound;
    Speed = speed;
    float angle = (float)(Math.PI / 4); // 45-degree arc

    float randomAngle = (float)((angle * 0.5) - (angle * random.NextDouble()));
    Vector2 direction = new Vector2((float)Math.Sin(randomAngle), (float)Math.Cos(randomAngle));
    Velocity = direction * speed;
  }

  public void Update(float deltaTime)
  {
    Position += Velocity * deltaTime;

    // Handle screen boundaries
    if (Position.X - Radius < 0 || Position.X + Radius > Raylib.GetScreenWidth())
    {
      Raylib.PlaySound(BounceSound);
      Velocity = new Vector2(-Velocity.X, Velocity.Y);
    }

    if (Position.Y - Radius < 0)
    {
      Raylib.PlaySound(BounceSound);
      Velocity = new Vector2(Velocity.X, -Velocity.Y);
    }
  }

  public void Draw()
  {
    Raylib.DrawCircleV(Position, Radius, Color);
  }

  public void Reset()
  {
    Position = initialPosition;
    float angle = (float)(Math.PI / 4); // 45-degree arc

    float randomAngle = (float)((angle * 0.5) - (angle * random.NextDouble()));
    Vector2 direction = new Vector2((float)Math.Sin(randomAngle), (float)Math.Cos(randomAngle));
    Velocity = direction * Speed;
  }
}
