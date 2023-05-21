using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

public class ParticleSystem
{
  private List<Particle> particles;

  public int MaxParticles { get; set; }
  public int BurstParticles { get; set; }
  public float ParticleRadius { get; set; }
  public float ParticleSpeed { get; set; }
  public Vector2 ParticleGravity { get; set; }
  public Color ParticleColor { get; set; }
  public float ParticleLifetime { get; set; }
  public float ParticleFadeoutTime { get; set; }

  public int ActiveParticles => particles.Count;

  public ParticleSystem()
  {
    particles = new List<Particle>();
  }

  public void Update(float deltaTime)
  {
    for (int i = particles.Count - 1; i >= 0; i--)
    {
      Particle particle = particles[i];
      particle.Position += particle.Velocity * deltaTime;
      particle.Velocity += ParticleGravity * deltaTime;
      particle.Lifetime -= deltaTime;

      if (particle.Lifetime <= 0)
        particles.RemoveAt(i);
    }

    if (particles.Count < MaxParticles)
    {
      int particlesToCreate = Math.Min(MaxParticles - particles.Count, BurstParticles);
      for (int i = 0; i < particlesToCreate; i++)
      {
        Particle particle = new Particle();
        particle.Position = new Vector2(Raylib.GetRandomValue(0, Raylib.GetScreenWidth()), Raylib.GetRandomValue(0, Raylib.GetScreenHeight()));
        particle.Velocity = new Vector2(Raylib.GetRandomValue(-1, 1), Raylib.GetRandomValue(-1, 1)) * ParticleSpeed;
        particle.Color = ParticleColor;
        particle.Radius = ParticleRadius;
        particle.Lifetime = ParticleLifetime;
        particles.Add(particle);
      }
    }
  }

  public void Draw()
  {
    foreach (Particle particle in particles)
    {
      float alpha = particle.Lifetime / ParticleLifetime;
      if (alpha > ParticleFadeoutTime)
        alpha = 1.0f;
      else
        alpha /= ParticleFadeoutTime;

      Color color = new Color(particle.Color.R, particle.Color.G, particle.Color.B, (byte)(particle.Color.A * alpha));
      Raylib.DrawCircleV(particle.Position, particle.Radius, color);
    }
  }
}
