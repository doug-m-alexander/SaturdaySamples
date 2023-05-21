using Raylib_cs;

class Window
{
  private double lastFrameTime;

  public int ScreenWidth { get; }
  public int ScreenHeight { get; }
  public string Title { get; }

  public Window(int width, int height, string title)
  {
    ScreenWidth = width;
    ScreenHeight = height;
    Title = title;
    lastFrameTime = Raylib.GetTime();

    Raylib.InitWindow(ScreenWidth, ScreenHeight, title);
  }

  public void BeginDrawing()
  {
    Raylib.BeginDrawing();
  }

  public void ClearBackground(Color color)
  {
    Raylib.ClearBackground(color);
  }

  public bool ShouldClose()
  {
    return Raylib.WindowShouldClose();
  }

  public double GetDeltaTime()
  {
    double currentTime = Raylib.GetTime();
    double deltaTime = currentTime - lastFrameTime;
    lastFrameTime = currentTime;

    return deltaTime;
  }

  public void EndDrawing()
  {
    Raylib.EndDrawing();
  }

  public void Close()
  {
    Raylib.CloseWindow();
  }
}
