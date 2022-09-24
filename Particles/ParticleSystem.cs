using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Particles;

public struct ParticleType {
    public Texture2D Texture { get; set; }

    public float SpeedMin { get; set; }
    public float SpeedMax { get; set; }
    public float SpeedIncrease { get; set; }

    public float DirectionMin { get; set; }
    public float DirectionMax { get; set; }
    public float DirectionIncrease { get; set; }

    public Vector2 Gravity { get; set; }

    public float RotationMin { get; set; }
    public float RotationMax { get; set; }

    public float RotationSpeedMin { get; set; }
    public float RotationSpeedMax { get; set; }
    public float RotationSpeedIncrease { get; set; }

    public Color Color { get; set; }

    public float SizeMin { get; set; }
    public float SizeMax { get; set; }
    public float SizeIncrease { get; set; }

    public int LifespanMin { get; set; }
    public int LifespanMax { get; set; }

    public ParticleType(Texture2D texture) { 
        Texture = texture;
        SpeedMin = SpeedMax = SpeedIncrease = DirectionMin = DirectionMax = DirectionIncrease = RotationMin = RotationMax =
            RotationSpeedMin = RotationSpeedMax = RotationSpeedIncrease = SizeIncrease = 0f;
        SizeMin = SizeMax = 1f;
        Color = Color.White;
        Gravity = Vector2.Zero;
        LifespanMin = LifespanMax = 30;
    }

    public void SetSpeed(float min, float max, float increase = 0f) {
        SpeedMin = min;
        SpeedMax = max;
        SpeedIncrease = increase;
    }

    public void SetDirection(float min, float max, float increase = 0f) {
        DirectionMin = min;
        DirectionMax = max;
        DirectionIncrease = increase;
    }

    public void SetGravity(float x, float y) {
        Gravity = new Vector2(x, y);
    }

    public void SetRotation(float min, float max) {
        RotationMin = min;
        RotationMax = max;
    }

    public void SetRotationSpeed(float min, float max, float increase = 0f) {
        RotationSpeedMin = min;
        RotationSpeedMax = max;
        RotationSpeedIncrease = increase;
    }

    public void SetSize(float min, float max, float increase = 0f) {
        SizeMin = min;
        SizeMax = max;
        SizeIncrease = increase;
    }

    public void SetLifespan(int min, int max) {
        LifespanMin = min;
        LifespanMax = max;
    }
}

public class ParticleSystem {
    private Random _rand;

    public Vector2 EmitterLocation { get; set; }

    private List<Particle> _particles;
    private Pool<Particle> _particlePool;

    public ParticleType ParticleType;

    public ParticleSystem(Texture2D texture, Vector2 location) {
        EmitterLocation = location;

        _particles = new List<Particle>();
        _particlePool = new Pool<Particle>(32);

        _rand = new Random();
        ParticleType = new ParticleType(texture);
    }

    public void Update() {
        var total = 1;
        var toRemove = new List<Particle>();

        for (var i = 0; i < total; i++) {
            _particles.Add(GenerateParticle());
        }

        foreach (var particle in _particles) {
            particle.Update();
            if (particle.Lifespan <= 0) {
                toRemove.Add(particle);
            }
        }

        foreach (var particle in toRemove) {
            _particlePool.Release(particle);
            _particles.Remove(particle);
        }
    }

    private Particle GenerateParticle() {
        var particle = _particlePool.Get();
        var pt = ParticleType;

        particle.Setup(
                pt.Texture,
                EmitterLocation,
                Utils.Lerp(pt.SpeedMin, pt.SpeedMax, (float)_rand.NextDouble()),
                Utils.Lerp(pt.DirectionMin, pt.DirectionMax, (float)_rand.NextDouble()),
                pt.Gravity,
                Utils.Lerp(pt.RotationMin, pt.RotationMax, (float)_rand.NextDouble()),
                Utils.Lerp(pt.RotationSpeedMin, pt.RotationSpeedMax, (float)_rand.NextDouble()),
                pt.Color,
                Utils.Lerp(pt.SizeMin, pt.SizeMax, (float)_rand.NextDouble()),
                (int)Utils.Lerp(pt.LifespanMin, pt.LifespanMax, (float)_rand.NextDouble()),
                pt.SpeedIncrease, pt.RotationSpeedIncrease, pt.SizeIncrease
                );

        return particle;
    }

    public void Render(SpriteBatch spriteBatch) {
        foreach (var particle in _particles) {
            particle.Render(spriteBatch);
        }
    }
}
