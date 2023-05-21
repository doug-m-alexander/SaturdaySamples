using Raylib_cs;
using System.Numerics;

class MainMenu
{
  private Window window;
  private Rectangle startButtonRect;
  private Rectangle optionsButtonRect;
  private Rectangle quitButtonRect;

  public GameState NextState { get; private set; }
  public MainMenu(Window window)
  {
    this.window = window;
    NextState = GameState.MainMenu;

    int buttonWidth = 250;
    int buttonHeight = 50;
    int buttonSpacing = 20;

    int centerX = window.ScreenWidth / 2;
    int centerY = window.ScreenHeight / 2;

    int buttonOffsetY = buttonHeight + buttonSpacing;

    startButtonRect = new Rectangle(centerX - buttonWidth / 2, centerY - buttonOffsetY, buttonWidth, buttonHeight);
    optionsButtonRect = new Rectangle(centerX - buttonWidth / 2, centerY, buttonWidth, buttonHeight);
    quitButtonRect = new Rectangle(centerX - buttonWidth / 2, centerY + buttonOffsetY, buttonWidth, buttonHeight);
  }

  public void Update()
  {
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
    {
      Vector2 mousePosition = Raylib.GetMousePosition();

      if (Raylib.CheckCollisionPointRec(mousePosition, startButtonRect))
      {
        NextState = GameState.Playing;
      }
      else if (Raylib.CheckCollisionPointRec(mousePosition, optionsButtonRect))
      {
        NextState = GameState.Options;
      }
      else if (Raylib.CheckCollisionPointRec(mousePosition, quitButtonRect))
      {
        NextState = GameState.Exit;
      }
    }
  }

  public void DrawButton(Rectangle buttonRect, string buttonText)
  {
    Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), buttonText, 40, 0);
    Vector2 textPosition = new Vector2(buttonRect.x + (buttonRect.width - textSize.X) / 2, buttonRect.y + (buttonRect.height - textSize.Y) / 2);
    Raylib.DrawText(buttonText, (int)textPosition.X, (int)textPosition.Y, 40, Color.LIGHTGRAY);
    Raylib.DrawRectangleLinesEx(buttonRect, 2, Color.LIGHTGRAY);
  }

  public void Draw()
  {
    window.ClearBackground(Color.DARKGRAY);

    DrawButton(startButtonRect, "START");
    DrawButton(optionsButtonRect, "OPTIONS");
    DrawButton(quitButtonRect, "QUIT");
  }

}
