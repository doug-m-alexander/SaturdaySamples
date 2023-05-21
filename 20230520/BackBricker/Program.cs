using Raylib_cs;
using System.Numerics;
using System.Threading.Tasks;

class Program
{
  static void Main()
  {
    Raylib.IsAudioDeviceReady();
    Raylib.InitAudioDevice();
    while (!Raylib.IsAudioDeviceReady())
    {
      Task.Delay(100);
    }

    // Load sound effects
    Sound bounceSound = Raylib.LoadSound("audio/percussive-hit-02_02-105799.ogg");
    Raylib.SetSoundVolume(bounceSound, 0.5f);
    Sound hitPaddleSound = Raylib.LoadSound("audio/soccer-ball-kick-37625.ogg");
    Raylib.SetSoundVolume(hitPaddleSound, 0.5f);
    Sound brickExplosionSound = Raylib.LoadSound("audio/pop2-84862.ogg");
    Raylib.SetSoundVolume(brickExplosionSound, 0.5f);

    Window window = new Window(1920, 1080, "Brick Breaker");
    Ball ball = new Ball(position: new Vector2(window.ScreenWidth / 2, window.ScreenHeight / 2), radius: 50, color: Color.VIOLET, bounceSound: bounceSound);
    Bricks bricks = new Bricks(rowCount: 1, columnCount: 5, brickWidth: 70, brickHeight: 30, brickPadding: 15, brickOffsetTop: 50, brickOffsetLeft: 50, ball: ball, deathSound: brickExplosionSound);
    Paddle paddle = new Paddle(x: window.ScreenWidth / 2 - 100 / 2, y: window.ScreenHeight - 20 - 10, width: 1920, height: 20, speed: 1600);

    Raylib.InitWindow(window.ScreenWidth, window.ScreenHeight, window.Title);
    Raylib.SetTargetFPS(300);

    bool isPlaying = false;
    bool isGameOver = false;
    bool hasWon = false;
    int lives = 3;

    // Create fireworks particles
    ParticleSystem fireworks = new ParticleSystem()
    {
      MaxParticles = 2000,
      BurstParticles = 500,
      ParticleRadius = 2,
      ParticleSpeed = 100,
      ParticleGravity = new Vector2(0, 200),
      ParticleColor = Color.ORANGE,
      ParticleLifetime = 2.0f,
      ParticleFadeoutTime = 1.8f
    };

    // Load background music
    Music bgMusic = Raylib.LoadMusicStream("audio/game-music-loop-3-144252.ogg");
    //Raylib.PlayMusicStream(bgMusic);

    while (!Raylib.WindowShouldClose())
    {
      float deltaTime = Raylib.GetFrameTime();
      if (isPlaying)
      {
        Raylib.UpdateMusicStream(bgMusic);
        

        paddle.Update(deltaTime);
        ball.Update(deltaTime);

        if (Raylib.CheckCollisionCircleRec(ball.Position, ball.Radius, paddle.Bounds))
        {
          // Ball and paddle collision handling
          ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
          Raylib.PlaySound(bounceSound);
        }

        if (bricks.AllBricksDestroyed())
        {
          isPlaying = false;
          hasWon = true;
        }

        if (ball.Position.Y - ball.Radius > Raylib.GetScreenHeight())
        {
          lives--;
          if (lives <= 0)
          {
            isPlaying = false;
            isGameOver = true;
          }
          else
          {
            paddle.Reset();
            ball.Reset();
          }
        }
      }
      else
      {
        if (isGameOver)
        {
          if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
          {
            Raylib.SeekMusicStream(bgMusic, 0);
            paddle.Reset();
            ball.Reset();
            bricks.Reset(ball);
            lives = 3;
            isGameOver = false;
            hasWon = false;
          }
        }
        else
        {
          if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
          {
            isPlaying = true;
            hasWon = false;
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
        DrawLives(window, lives);
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
        else if (hasWon)
        {
          string congratulationsText = "Congratulations! You Won!";
          int congratulationsTextWidth = Raylib.MeasureText(congratulationsText, 60);
          int congratulationsTextX = window.ScreenWidth / 2 - congratulationsTextWidth / 2;
          int congratulationsTextY = window.ScreenHeight / 2 - 60 / 2;
          Raylib.DrawText(congratulationsText, congratulationsTextX, congratulationsTextY, 60, Color.GREEN);

          

          fireworks.Emit(congratulationsTextX + 250, congratulationsTextY + 250); // Emit fireworks particles at the specified position
          fireworks.Update(deltaTime); // Update fireworks particles

          // Draw fireworks particles
          fireworks.Draw();
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

    // Unload resources
    Raylib.UnloadMusicStream(bgMusic);
    Raylib.UnloadSound(bounceSound);
    //Raylib.UnloadSound(hitPaddleSound);

    Raylib.CloseWindow();
  }

  static void DrawLives(Window window, int lives)
  {
    if (lives <= 0)
      return;

    const int ballRadius = 10;
    const int spacing = 5;
    Color lifeColor = new Color(255, 0, 0, 200);

    for (int i = 0; i < lives; i++)
    {
      int x = 20 + (ballRadius * 2 + spacing) * i;
      int y = 20;

      Raylib.DrawCircle(x, y, ballRadius, lifeColor);
    }
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
