using Raylib_cs;
using System;
using System.Threading.Tasks;

class Victory
{
  private Window window;
  private ParticleSystem fireworks;

  private bool victorious = false;

  private float timeSinceWin = 0;

  private bool[] winSounds = new bool[5];

  public Victory(Window window)
  {
    this.window = window;
    fireworks = new ParticleSystem()
    {
      MaxParticles = 2000,
      BurstParticles = 50,
      ParticleRadius = 2,
      ParticleSpeed = 100,
      ParticleLifetime = 2.0f,
      ParticleFadeoutTime = 1.8f
    };
  }

  public void Update(float deltaTime)
  {
    fireworks.Update(deltaTime);
    if(victorious)
    {
      timeSinceWin += deltaTime;
    }
  }

  public void Draw()
  {
    window.ClearBackground(Color.BLACK);

    fireworks.Emit();
    fireworks.Draw();

    string congratulationsText = "Congratulations! You Won!";
    int congratulationsTextSize = 60;

    int congratulationsTextWidth = Raylib.MeasureText(congratulationsText, congratulationsTextSize);
    int congratulationsTextX = window.ScreenWidth / 2 - congratulationsTextWidth / 2;
    int congratulationsTextY = window.ScreenHeight / 2 - congratulationsTextSize / 2;

    // Configure the outline offset
    int outlineOffset = 2;

    Color currentColor = Color.RAYWHITE;

    // Draw the text layers with offsets to create an outline effect
    Raylib.DrawText(congratulationsText, congratulationsTextX - outlineOffset, congratulationsTextY - outlineOffset, congratulationsTextSize, currentColor); // Top-left
    Raylib.DrawText(congratulationsText, congratulationsTextX + outlineOffset, congratulationsTextY - outlineOffset, congratulationsTextSize, currentColor); // Top-right
    Raylib.DrawText(congratulationsText, congratulationsTextX - outlineOffset, congratulationsTextY + outlineOffset, congratulationsTextSize, currentColor); // Bottom-left
    Raylib.DrawText(congratulationsText, congratulationsTextX + outlineOffset, congratulationsTextY + outlineOffset, congratulationsTextSize, currentColor); // Bottom-right

    // Draw the main text in green
    Raylib.DrawText(congratulationsText, congratulationsTextX, congratulationsTextY, congratulationsTextSize, Color.GREEN);
  }

  internal void StartVictory()
  {
    if (!winSounds[0])
    {
      // Play these two first together
      Sound winFantasia = Raylib.LoadSound("audio/winning/winfantasia-6912.ogg");
      Sound yay = Raylib.LoadSound("audio/winning/yay-6120.ogg");
      Raylib.PlaySound(winFantasia);
      Raylib.PlaySound(yay);
      winSounds[0] = true;

    }

    if (!winSounds[1] && timeSinceWin < 2.0f)
    {
      Sound shortCrowd = Raylib.LoadSound("audio/winning/short-crowd-cheer-6713.ogg");
      Raylib.PlaySound(shortCrowd);
      winSounds[1] = true;
    }

    if (!winSounds[2] && timeSinceWin < 2.5f)
    {
      Sound cartoonYay = Raylib.LoadSound("audio/winning/cartoon-yay-140921.ogg");
      Raylib.PlaySound(cartoonYay);
      winSounds[2] = true;
    }

    if (!winSounds[3] && timeSinceWin < 3.0f)
    {
      Sound funnyYay = Raylib.LoadSound("audio/winning/funny-yay-6273.ogg");
      Raylib.PlaySound(funnyYay);
      winSounds[3] = true;
    }

    if (!winSounds[4] && timeSinceWin < 10.0f)
    {
      Sound shortCrowd2 = Raylib.LoadSound("audio/winning/short-crowd-cheer-2-88701");
      Raylib.PlaySound(shortCrowd2);
      winSounds[4] = true;
    }
  }
}
