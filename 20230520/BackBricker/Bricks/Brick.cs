using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BackBricker.Bricks
{
  public class Brick
  {
    public bool IsVisible { get; set; }
    public Rectangle Bounds { get; }
    public Color Color { get; }
    private const int BorderThickness = 2;

    public Brick(Rectangle bounds, Color color)
    {
      Bounds = bounds;
      Color = color;
      IsVisible = true;
    }

    public void DrawBrick()
    {
      if (IsVisible)
      {
        Raylib.DrawRectangleRec(Bounds, Color);
        Raylib.DrawRectangleLinesEx(Bounds, BorderThickness, Color.BLACK);
      }
    }

    public void Update(Ball ball)
    {
      if (IsVisible && Raylib.CheckCollisionCircleRec(ball.Position, ball.Radius, Bounds))
      {
        IsVisible = false;

        // Ball is within the bounds of the brick
        Vector2 ballDirection = ball.Velocity;
        Vector2 brickCenter = new Vector2(Bounds.x + Bounds.width / 2, Bounds.y + Bounds.height / 2);
        Vector2 collisionNormal = Vector2.Normalize(ball.Position - brickCenter);

        // Reflect the ball's direction based on the collision normal
        ball.Velocity = Vector2.Reflect(ballDirection, collisionNormal);
      }
    }
  }
}
