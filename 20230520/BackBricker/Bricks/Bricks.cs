using BackBricker;
using BackBricker.Bricks;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Bricks
{
  private List<Brick> bricks;

  public Bricks(Ball ball, Sound brickExplosionSound)
  {
    bricks = LoadBricksFromJson("levels/default.json", brickExplosionSound);
    InitializeBricks(ball);
  }

  public static List<Brick> LoadBricksFromJson(string filePath, Sound brickExplosionSound)
  {
    Random r = new Random();
    // Read the JSON file
    string json = File.ReadAllText(filePath);

    // Deserialize the JSON data into a list of BrickData objects
    BrickTileset brickDataList = JsonSerializer.Deserialize<BrickTileset>(json, options: new JsonSerializerOptions()
    {
      PropertyNameCaseInsensitive = true,
    });

    var leftOffset = 75;
    var topOffset = 45;

    // Create Brick instances from the BrickData objects
    List<Brick> bricks = new List<Brick>();
    var cumulativeVertical = 0;
    foreach (var brickData in brickDataList.BlockMap)
    {
      var horPadding = brickData.HorizontalPadding;
      var vertPadding = brickData.VerticalPadding;
      for (int i = 0; i < brickData.Rows; i++)
      {
        var tallestBrick = 0;
        var cumulativeHorizontal = 0;
        for (int j = 0; j < brickData.BlockPattern.Count; j++)
        {
          var brickType = brickDataList.BlockTypes[brickData.BlockPattern[j]];
          var rect = new Rectangle(leftOffset + cumulativeHorizontal, topOffset + cumulativeVertical, brickType.Size.Width, brickType.Size.Height);
          cumulativeHorizontal += brickType.Size.Width + horPadding;
          var c = brickType.BrickColors[r.Next(brickType.BrickColors.Count)];
          var color = new Color(c.R, c.G, c.B, c.A);
          // Create a Brick instance using the BrickData properties
          Brick brick = new Brick(rect, color, brickExplosionSound);
          // Add the Brick instance to the list
          bricks.Add(brick);
          if(brickType.Size.Height > tallestBrick)
          {
            tallestBrick = brickType.Size.Height;
          }
        }
        cumulativeVertical += vertPadding + tallestBrick;
      }

    }

    return bricks;
  }


  private void InitializeBricks(Ball ball)
  {
    DrawAndUpdate(ball);
  }

  public void DrawAndUpdate(Ball ball)
  {
    foreach (var brick in bricks)
    {
      brick.Update(ball);
      brick.DrawBrick();
    }
  }

  public bool AllBricksDestroyed()
  {
    return bricks.TrueForAll(x => false == x.IsVisible);
  }

  public void Reset(Ball ball)
  {
    foreach (var brick in bricks)
    {
      brick.IsVisible = true;
    }
  }
}
