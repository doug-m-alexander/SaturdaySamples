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
  public Vector2 ParticleGravity { get; set; }
  public Color ParticleColor { get; set; }
  public float ParticleLifetime { get; set; }
  public float ParticleFadeoutTime { get; set; }

  private List<Particle> particles;

  public ParticleSystem()
  {
    particles = new List<Particle>();
  }

  public void Emit(float x, float y)
  {
    Random random = new Random();

    for (int i = 0; i < BurstParticles; i++)
    {
      float angle = (float)(random.NextDouble() * 2 * Math.PI);
      float speed = (float)(random.NextDouble() * ParticleSpeed);

      Vector2 velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed);
      Particle particle = new Particle(new Vector2(x, y), velocity, ParticleRadius, ParticleColor, ParticleLifetime, ParticleFadeoutTime, ParticleGravity);

      particles.Add(particle);
    }
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
