using Raylib_cs;

namespace BackBricker
{
  internal class BrickMethods
  {
    public static void DrawBricks(int[,] bricks, int rowCount, int columnCount, int brickWidth, int brickHeight, int brickPadding, int brickOffsetTop, int brickOffsetLeft)
    {
      int totalBrickWidth = columnCount * brickWidth + (columnCount - 1) * brickPadding;

      for (int row = 0; row < rowCount; row++)
      {
        for (int col = 0; col < columnCount; col++)
        {
          int brickX = col * (brickWidth + brickPadding) + brickOffsetLeft;
          int brickY = row * (brickHeight + brickPadding) + brickOffsetTop;

          if (bricks[row, col] == 1)
          {
            Raylib.DrawRectangle(brickX, brickY, brickWidth, brickHeight, Color.RED);
          }
        }
      }
    }

  }
}
