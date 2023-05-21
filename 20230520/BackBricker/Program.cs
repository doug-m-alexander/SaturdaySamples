using Raylib_cs;
using System.Numerics;

class Program
{
  static void Main()
  {
    Window window = new Window(1920, 1080, "Brick Breaker");
    Ball ball = new Ball(new Vector2(window.ScreenWidth / 2, window.ScreenHeight / 2), new Vector2(400, -400), 10, Color.VIOLET);
    Bricks bricks = new Bricks(rowCount: 5, columnCount: 20, brickWidth: 70, brickHeight: 30, brickPadding: 15, brickOffsetTop: 50, brickOffsetLeft: 50, ball: ball);
    Paddle paddle = new Paddle(window.ScreenWidth / 2 - 100 / 2, window.ScreenHeight - 20 - 10, 250, 20, 400);

    Raylib.InitWindow(window.ScreenWidth, window.ScreenHeight, window.Title);
    Raylib.SetTargetFPS(300);

    while (!Raylib.WindowShouldClose())
    {
      float deltaTime = Raylib.GetFrameTime();

      paddle.Update(deltaTime);
      ball.Update(deltaTime);

      if (Raylib.CheckCollisionCircleRec(ball.Position, ball.Radius, paddle.Bounds))
      {
        // Ball and paddle collision handling
        ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
      }

      window.BeginDrawing();
      window.ClearBackground(Color.RAYWHITE);

      bricks.DrawAndUpdate(ball);
      paddle.Draw();
      ball.Draw();

      DrawFPS(window);

      window.EndDrawing();
    }

    Raylib.CloseWindow();
  }

  static void DrawFPS(Window window)
  {
    int fps = Raylib.GetFPS();
    string fpsText = $"FPS: {fps.ToString()}";
    fpsText = fpsText.PadLeft(7, '0'); // Pad the FPS text with leading zeros
    int fontSize = 20; // Adjust the font size as needed
    int textX = window.ScreenWidth - (int)Raylib.MeasureText(fpsText, fontSize) - 20;
    int textY = 20;

    Raylib.DrawText(fpsText, textX, textY, fontSize, Color.GREEN);
  }
}
