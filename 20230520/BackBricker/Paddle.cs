using Raylib_cs;
using System;
using System.Numerics;
using System.Reflection.Metadata;

class Paddle
{
  public Rectangle Bounds { get; private set; }
  private Vector2 initialPosition;
  public Vector2 Position { get; set; }
  public Vector2 previousPosition;
  public Vector2 Velocity;
  public int Width { get; set; }
  public int Height { get; set; }
  public float Speed { get; set; }


  public Paddle(float x, float y, int width, int height, float speed)
  {
    initialPosition = new Vector2(x, y);
    Width = width;
    Height = height;
    Speed = speed;
    Bounds = new Rectangle(x, y, width, height);

    Reset();
  }

  public void Update(float deltaTime)
  {
    float direction = 0;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
      direction = -1;
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
      direction = 1;

    Vector2 newPosition = Position;
    newPosition.X += direction * Speed * deltaTime;
    newPosition.X = Math.Clamp(newPosition.X, 0, Raylib.GetScreenWidth() - Width);

    previousPosition = Position;
    Position = newPosition;
    Velocity = Position - previousPosition;
    Bounds = new Rectangle(Position.X, Position.Y, Width, Height);
  }

  public void Reset()
  {
    Position = initialPosition;
    previousPosition = Position;
    Velocity = new Vector2(0, 0);
  }

  public void Draw()
  {
    Raylib.DrawRectangleRec(Bounds, Color.BLUE);
  }
}
