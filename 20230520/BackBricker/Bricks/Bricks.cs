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
  private Brick[,] bricks;

  public Bricks(int rowCount, int columnCount, int brickWidth, int brickHeight, int brickPadding, int brickOffsetTop, int brickOffsetLeft, Ball ball)
  {
    this.rowCount = rowCount;
    this.columnCount = columnCount;
    this.brickWidth = brickWidth;
    this.brickHeight = brickHeight;
    this.brickPadding = brickPadding;
    this.brickOffsetTop = brickOffsetTop;
    this.brickOffsetLeft = brickOffsetLeft;

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
        bricks[row, col] = new Brick(new Rectangle(brickX, brickY, brickWidth, brickHeight), Raylib.ColorFromHSV(hue, saturation, value)); // 1 indicates that the brick is not destroyed
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
}
