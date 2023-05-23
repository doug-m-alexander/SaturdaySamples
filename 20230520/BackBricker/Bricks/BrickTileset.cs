using System.Collections.Generic;

namespace BackBricker
{
  public record BrickTileset
  {
    public Metadata Metadata { get; init; }
    public List<BlockType> BlockTypes { get; init; }
    public List<BlockMapEntry> BlockMap { get; init; }
  }

  public record Metadata
  {
    public string BackgroundMusic { get; init; }
    public string BackgroundImage { get; init; }
  }

  public record BlockType
  {
    public string Name { get; init; }
    public Size Size { get; init; }
    public List<BrickColor> BrickColors { get; init; }
    public string DeathSound { get; init; }
  }

  public record Size
  {
    public int Width { get; init; }
    public int Height { get; init; }
  }

  public record BrickColor
  {
    public string Name { get; init; }
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public byte A { get; init; }
  }

  public record BlockMapEntry
  {
    public int Rows { get; init; }
    public int VerticalPadding { get; init; }
    public int HorizontalPadding { get; init; }
    public List<int> BlockPattern { get; init; }
  }

}
