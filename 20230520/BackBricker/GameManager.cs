using Raylib_cs;
using System;
using System.Numerics;
using System.Threading.Tasks;



class GameManager
{
  private Window window;
  private Ball ball;
  private Bricks bricks;
  private Paddle paddle;
  private ParticleSystem fireworks;

  private Music bgMusic;
  private Sound bounceSound;

  private int lives;
  private GameState gameState;

  private MainMenu mainMenu;
  private Victory victory;

  public GameManager(Music bgMusic, Sound bounceSound, Sound brickExplosionSound)
  {
    window = new Window(1920, 1080, "BackBricker");
    ball = new Ball(position: new Vector2(window.ScreenWidth / 2, window.ScreenHeight / 2), radius: 15, color: Color.VIOLET, bounceSound: bounceSound, speed: 500);
    bricks = new Bricks(rowCount: 6, columnCount: 10, brickWidth: 157, brickHeight: 50, brickPadding: 10, brickOffsetTop: 45, brickOffsetLeft: 125, ball: ball, deathSound: brickExplosionSound);
    paddle = new Paddle(x: window.ScreenWidth / 2 - 100 / 2, y: window.ScreenHeight - 20 - 10, width: 200, height: 20, speed: 555);

    lives = 3;
    gameState = GameState.MainMenu;

    this.bgMusic = bgMusic;
    this.bounceSound = bounceSound;

    mainMenu = new MainMenu(window);
    victory = new Victory(window);
  }

  public void Run()
  {
    Raylib.InitWindow(window.ScreenWidth, window.ScreenHeight, window.Title);
    Raylib.SetTargetFPS(300);

    while (!Raylib.WindowShouldClose())
    {
      float deltaTime = Raylib.GetFrameTime();

      switch (gameState)
      {
        case GameState.MainMenu:
          UpdateMainMenu();
          break;
        case GameState.Playing:
          UpdatePlaying(deltaTime);
          break;
        case GameState.GameOver:
          UpdateGameOver(deltaTime);
          break;
        case GameState.Victory:
          victory.StartVictory();
          UpdateVictory(deltaTime);
          break;
        case GameState.Options:
          break;
        case GameState.Exit:
          Raylib.CloseWindow();
          break;
      }

      Draw();
    }

    Raylib.CloseWindow();
  }

  private void UpdateMainMenu()
  {
    mainMenu.Update();
    switch (mainMenu.NextState)
    {
      case GameState.MainMenu:
        break;
      case GameState.Playing:
        gameState = GameState.Playing;
        break;
      case GameState.Options:
        gameState = GameState.Options;
        break;
      case GameState.Exit:
        gameState = GameState.Exit;
        break;
    }
  }

  private void UpdatePlaying(float deltaTime)
  {
    paddle.Update(deltaTime);
    ball.Update(deltaTime);
    Raylib.UpdateMusicStream(bgMusic);

    if (Raylib.CheckCollisionCircleRec(ball.Position, ball.Radius, paddle.Bounds))
    {
      ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
      Raylib.PlaySound(bounceSound);
    }

    if (bricks.AllBricksDestroyed())
    {
      gameState = GameState.Victory;
    }

    if (ball.Position.Y - ball.Radius > Raylib.GetScreenHeight())
    {
      lives--;
      if (lives <= 0)
      {
        gameState = GameState.GameOver;
      }
      else
      {
        paddle.Reset();
        ball.Reset();
      }
    }
  }

  private void UpdateGameOver(float deltaTime)
  {
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
    {
      Raylib.SeekMusicStream(bgMusic, 0);
      paddle.Reset();
      ball.Reset();
      bricks.Reset(ball);
      lives = 3;
      gameState = GameState.MainMenu;
    }
  }

  private void UpdateVictory(float deltaTime)
  {
    victory.Update(deltaTime);
  }

  private void Draw()
  {
    window.BeginDrawing();
    window.ClearBackground(Color.LIGHTGRAY);

    switch (gameState)
    {
      case GameState.Playing:
        DrawPlayingState();
        break;
      case GameState.GameOver:
        DrawGameOverState();
        break;
      case GameState.Victory:
        victory.Draw();
        break;
      case GameState.MainMenu:
        mainMenu.Draw();
        break;
    }

    DrawFPS();

    window.EndDrawing();
  }

  private void DrawPlayingState()
  {
    bricks.DrawAndUpdate(ball);
    paddle.Draw();
    ball.Draw();
    DrawLives();
  }

  private void DrawGameOverState()
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

  private void DrawLives()
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

  private void DrawFPS()
  {
    int fps = Raylib.GetFPS();
    string fpsText = $"FPS: {fps}";
    fpsText = fpsText.PadLeft(7, '0');
    int fontSize = 20;
    int textX = window.ScreenWidth - Raylib.MeasureText(fpsText, fontSize) - 20;
    int textY = 20;

    Raylib.DrawText(fpsText, textX, textY, fontSize, Color.GREEN);
  }
}
