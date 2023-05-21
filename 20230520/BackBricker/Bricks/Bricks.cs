using BackBricker.Bricks;
using Raylib_cs;
using System;

class Bricks
{
  private int rowCount;
  private int columnCount;
  private int brickWidth;
  private int brickHeight;
  private int brickPadding;
  private int brickOffsetTop;
  private int brickOffsetLeft;
  private readonly Sound deathSound;
  private Brick[,] bricks;

  private Random random = new Random();

  Color[] vibrantColors = new Color[]
{
    new Color(255, 0, 0, 255),           // Red
    new Color(255, 165, 0, 255),         // Orange
    new Color(255, 255, 0, 255),         // Yellow
    new Color(0, 255, 0, 255),           // Lime
    new Color(0, 255, 255, 255),         // Cyan
    new Color(255, 0, 255, 255),         // Magenta
    new Color(255, 105, 180, 255),       // Pink
    new Color(128, 0, 128, 255),         // Purple
    new Color(255, 20, 147, 255),        // Deep Pink
    new Color(127, 255, 0, 255),         // Chartreuse
    new Color(0, 255, 127, 255),         // Spring Green
    new Color(0, 255, 255, 255),         // Aqua
    new Color(0, 191, 255, 255),         // Deep Sky Blue
    new Color(138, 43, 226, 255),        // Blue Violet
    new Color(255, 0, 255, 255),         // Magenta
    new Color(205, 92, 92, 255),         // Indian Red
    new Color(255, 215, 0, 255),         // Gold
    new Color(124, 252, 0, 255),         // Lawn Green
    new Color(100, 149, 237, 255),       // Cornflower Blue
    new Color(147, 112, 219, 255),       // Medium Purple
    new Color(32, 178, 170, 255),        // Light Sea Green
    new Color(186, 85, 211, 255),        // Medium Orchid
    new Color(255, 69, 0, 255),          // Orange Red
    new Color(199, 21, 133, 255),        // Medium Violet Red
    new Color(244, 164, 96, 255)         // Sandy Brown
};




  public Bricks(int rowCount, int columnCount, int brickWidth, int brickHeight, int brickPadding, int brickOffsetTop, int brickOffsetLeft, Ball ball, Sound deathSound)
  {
    this.rowCount = rowCount;
    this.columnCount = columnCount;
    this.brickWidth = brickWidth;
    this.brickHeight = brickHeight;
    this.brickPadding = brickPadding;
    this.brickOffsetTop = brickOffsetTop;
    this.brickOffsetLeft = brickOffsetLeft;
    this.deathSound = deathSound;
    bricks = new Brick[rowCount, columnCount];
    InitializeBricks(ball);
  }

  private void InitializeBricks(Ball ball)
  {
    Random r = new Random();
    for (int row = 0; row < rowCount; row++)
    {
      for (int col = 0; col < columnCount; col++)
      {
        float hue = (float)r.NextDouble() * 360.0f;
        float saturation = (float)r.NextDouble();
        float value = (float)r.NextDouble();

        int brickX = col * (brickWidth + brickPadding) + brickOffsetLeft;
        int brickY = row * (brickHeight + brickPadding) + brickOffsetTop;
        bricks[row, col] = new Brick(new Rectangle(brickX, brickY, brickWidth, brickHeight), vibrantColors[random.Next(25)], deathSound: deathSound); // 1 indicates that the brick is not destroyed
      }
    }

    DrawAndUpdate(ball);
  }

  public void DrawAndUpdate(Ball ball)
  {
    for (int row = 0; row < rowCount; row++)
    {
      for (int col = 0; col < columnCount; col++)
      {
        Brick brick = bricks[row, col];
        if (brick != null && brick.IsVisible)
        {
          brick.Update(ball);
          brick.DrawBrick();
        }
      }
    }
  }

  public bool AllBricksDestroyed()
  {
    for (int row = 0; row < rowCount; row++)
    {
      for (int col = 0; col < columnCount; col++)
      {
        if (bricks[row, col] != null && bricks[row, col].IsVisible)
        {
          return false; // At least one brick is still visible
        }
      }
    }

    return true; // All bricks are destroyed
  }

  public void Reset(Ball ball)
  {
    InitializeBricks(ball);
  }
}
