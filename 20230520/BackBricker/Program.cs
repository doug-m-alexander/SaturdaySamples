using Raylib_cs;
using System.Numerics;

class Program
{
  static void Main()
  {
    Window window = new Window(1920, 1080, "Brick Breaker");
    Ball ball = new Ball(new Vector2(window.ScreenWidth / 2, window.ScreenHeight / 2), 10, Color.VIOLET);
    Bricks bricks = new Bricks(rowCount: 5, columnCount: 20, brickWidth: 70, brickHeight: 30, brickPadding: 15, brickOffsetTop: 50, brickOffsetLeft: 50, ball: ball);
    Paddle paddle = new Paddle(window.ScreenWidth / 2 - 100 / 2, window.ScreenHeight - 20 - 10, 250, 20, 400);

    Raylib.InitWindow(window.ScreenWidth, window.ScreenHeight, window.Title);
    Raylib.SetTargetFPS(300);

    bool isPlaying = false;
    bool isGameOver = false;

    while (!Raylib.WindowShouldClose())
    {
      if (isPlaying)
      {
        float deltaTime = Raylib.GetFrameTime();

        paddle.Update(deltaTime);
        ball.Update(deltaTime);

        if (Raylib.CheckCollisionCircleRec(ball.Position, ball.Radius, paddle.Bounds))
        {
          // Ball and paddle collision handling
          ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
        }

        if (bricks.AllBricksDestroyed())
        {
          isPlaying = false;
          isGameOver = true;
        }

        if (ball.Position.Y - ball.Radius > Raylib.GetScreenHeight())
        {
          isPlaying = false;
          isGameOver = true;
        }
      }
      else
      {
        if (isGameOver)
        {
          if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
          {
            paddle.Reset();
            ball.Reset();
            bricks.Reset(ball);
            isGameOver = false;
          }
        }
        else
        {
          if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
          {
            isPlaying = true;
          }
        }
      }

      window.BeginDrawing();
      window.ClearBackground(Color.RAYWHITE);

      if (isPlaying)
      {
        bricks.DrawAndUpdate(ball);
        paddle.Draw();
        ball.Draw();
      }
      else
      {
        if (isGameOver)
        {
          string gameOverText = "Game Over";
          int gameOverTextWidth = Raylib.MeasureText(gameOverText, 60);
          int gameOverTextX = window.ScreenWidth / 2 - gameOverTextWidth / 2;
          int gameOverTextY = window.ScreenHeight / 2 - 60 / 2;
          Raylib.DrawText(gameOverText, gameOverTextX, gameOverTextY, 60, Color.RED);

          string restartText = "Press ENTER to restart";
          int restartTextWidth = Raylib.MeasureText(restartText, 30);
          int restartTextX = window.ScreenWidth / 2 - restartTextWidth / 2;
          int restartTextY = gameOverTextY + 60 + 20;
          Raylib.DrawText(restartText, restartTextX, restartTextY, 30, Color.BLACK);
        }
        else
        {
          string startText = "Press ENTER to start";
          int startTextWidth = Raylib.MeasureText(startText, 40);
          int startTextX = window.ScreenWidth / 2 - startTextWidth / 2;
          int startTextY = window.ScreenHeight / 2 - 40 / 2;
          Raylib.DrawText(startText, startTextX, startTextY, 40, Color.DARKBLUE);
        }
      }

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
