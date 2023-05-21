public class BrickData
{
  public Bounds Bounds { get; set; }
  public LoadColor Color { get; set; }
  public string DeathSound { get; set; }
}

public class Bounds
{
  public int x { get; set; }
  public int y { get; set; }
  public int width { get; set; }
  public int height { get; set; }
}

public class LoadColor
{
  public int r { get; set; }
  public int g { get; set; }
  public int b { get; set; }
  public int a { get; set; }
}
