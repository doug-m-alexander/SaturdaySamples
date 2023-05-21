using Raylib_cs;
using System.Numerics;

public class Ball
{
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Radius { get; set; }
  public Color Color { get; set; }

  public Ball(Vector2 position, Vector2 velocity, float radius, Color color)
  {
    Position = position;
    Velocity = velocity;
    Radius = radius;
    Color = color;
  }

  public void Update(float deltaTime)
  {
    Position += Velocity * deltaTime;

    if (Position.Y - Radius > Raylib.GetScreenHeight())
    {
      // Ball went out of the bottom of the screen, reset its position
      Position = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
    }
    else
    {
      if (Position.X - Radius < 0 || Position.X + Radius > Raylib.GetScreenWidth())
        Velocity = new Vector2(-Velocity.X, Velocity.Y);

      if (Position.Y - Radius < 0 )//|| Position.Y + Radius > Raylib.GetScreenHeight())
        Velocity = new Vector2(Velocity.X, -Velocity.Y);
    }
  }




  public void Draw()
  {
    Raylib.DrawCircleV(Position, Radius, Color);
  }
}
