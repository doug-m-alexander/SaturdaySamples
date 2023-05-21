using BackBricker.Bricks;
using Raylib_cs;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Bricks
{
  private List<Brick> bricks;

  public Bricks(Ball ball, Sound brickExplosionSound)
  {
    bricks = LoadBricksFromJson("levels/brickTileset.json", brickExplosionSound);
    InitializeBricks(ball);
  }

  public static List<Brick> LoadBricksFromJson(string filePath, Sound brickExplosionSound)
  {
    // Read the JSON file
    string json = File.ReadAllText(filePath);

    // Deserialize the JSON data into a list of BrickData objects
    List<BrickData> brickDataList = JsonSerializer.Deserialize<List<BrickData>>(json, options: new JsonSerializerOptions()
    {
      PropertyNameCaseInsensitive = true,
    });

    // Create Brick instances from the BrickData objects
    List<Brick> bricks = new List<Brick>();
    foreach (var brickData in brickDataList)
    {
      // Create a Brick instance using the BrickData properties
      Brick brick = new Brick(new Rectangle(brickData.Bounds.x, brickData.Bounds.y, brickData.Bounds.width, brickData.Bounds.height), 
        new Color(brickData.Color.r, brickData.Color.g, brickData.Color.b, brickData.Color.a), brickExplosionSound);

      // Add the Brick instance to the list
      bricks.Add(brick);
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
