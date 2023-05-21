using Raylib_cs;
using System.Threading.Tasks;

class Program
{
  static void Main()
  {
    Raylib.IsAudioDeviceReady();
    Raylib.InitAudioDevice();
    while (!Raylib.IsAudioDeviceReady())
    {
      Task.Delay(100);
    }

    Sound bounceSound = Raylib.LoadSound("audio/percussive-hit-02_02-105799.ogg");
    Sound brickExplosionSound = Raylib.LoadSound("audio/pop2-84862.ogg");
    Music bgMusic = Raylib.LoadMusicStream("audio/game-music-loop-3-144252.ogg");
    Raylib.PlayMusicStream(bgMusic);

    GameManager gameManager = new GameManager(bgMusic, bounceSound, brickExplosionSound);
    gameManager.Run();

    // Unload resources
    Raylib.StopMusicStream(bgMusic);
    Raylib.UnloadMusicStream(bgMusic);
    Raylib.UnloadSound(bounceSound);
    Raylib.UnloadSound(brickExplosionSound);
  }
}
