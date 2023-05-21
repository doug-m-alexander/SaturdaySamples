using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

class ParticleSystem
{
  public int MaxParticles { get; set; }
  public int BurstParticles { get; set; }
  public float ParticleRadius { get; set; }
  public float ParticleSpeed { get; set; }
  public float ParticleLifetime { get; set; }
  public float ParticleFadeoutTime { get; set; }

  private List<Particle> particles;

  private Random random = new Random();

  public ParticleSystem()
  {
    particles = new List<Particle>();
  }

  public void Emit()
  {

    for (int i = 0; i < BurstParticles; i++)
    {
      float angle = (float)(random.NextDouble() * 2 * Math.PI);
      float speed = (float)(random.NextDouble() * ParticleSpeed);

      Vector2 velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed);
      Particle particle = new Particle(GetRandomPosition(), velocity, ParticleRadius, GetRandomColor(), ParticleLifetime, ParticleFadeoutTime, GetRandomGravity());

      particles.Add(particle);
    }
  }

  private Vector2 GetRandomPosition()
  {
    return new Vector2(random.Next(1920), random.Next(1080));
  }

  private Vector2 GetRandomGravity()
  {
    return new Vector2(random.Next(-400, 400), random.Next(-400, 400));
  }

  private Color GetRandomColor()
  {
    byte r = (byte)random.Next(256);
    byte g = (byte)random.Next(256);
    byte b = (byte)random.Next(256);
    return new Color(r, g, b, (byte)255);
  }


  public void Update(float deltaTime)
  {
    for (int i = particles.Count - 1; i >= 0; i--)
    {
      Particle particle = particles[i];
      particle.Update(deltaTime);

      if (particle.Lifetime <= 0)
      {
        particles.RemoveAt(i);
      }
    }
  }

  public void Draw()
  {
    foreach (Particle particle in particles)
    {
      particle.Draw();
    }
  }
}
