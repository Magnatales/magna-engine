using System.Numerics;
using Actors;
using Raylib_cs;

namespace Core.modules.particles_system;

public class ParticleEmitter : Actor
{
    private List<Particle> _particles = new();
    //private int _particleIndex;
    
    public static int Count;
    private readonly IParticleEmitter _emitter;
    private readonly ParticleData _data;
    private float _intervalLeft;
    
    public ParticleEmitter(IParticleEmitter emitter, ParticleData data) : base(emitter.Position)
    {
        _emitter = emitter;
        _data = data;
        _intervalLeft = _data.EmitData.Interval;
        //_particles = new Particle[_emitterData.MaxParticles];
        //Count += _emitterData.MaxParticles;
    }

    public override void Update(float dt)
    {
        _intervalLeft -= dt;
        if(_intervalLeft <= 0)
        {
            _intervalLeft =_data.EmitData.Interval;
            for (int i = 0; i < _data.EmitData.EmitCount; i++)
            {
                AddParticle(_emitter.Position);
            }
        }

        for (var index = _particles.Count - 1; index >= 0; index--)
        {
            var particle = _particles[index];
            particle.LifespanLeft -= dt;
            if(particle.LifespanLeft <= 0f)
            {
                Count--;
                _particles.Remove(particle);
                continue;
            }

            if (_data.PhysicsData.Gravity != 0)
            {
                particle.Direction.Y += _data.PhysicsData.Gravity * dt;

                if (particle.Position.Y > 230f)
                {
                    particle.Direction = new Vector2(particle.Direction.X, 0);
                    particle.Direction *= 1.0f - _data.PhysicsData.Friction * dt;
                }
            }
            particle.Rotation += particle.RotationSpeed * dt;
            particle.LifespanAmount = Raymath.Clamp(particle.LifespanLeft / particle.Lifespan, 0, 1);
            particle.Color = Lerp(_data.ColorData.ColorEnd, _data.ColorData.ColorStart, particle.LifespanAmount);
            particle.Scale = Raymath.Lerp(_data.SizeData.SizeEnd, particle.StartingScale, particle.LifespanAmount) / _data.Texture.Width;
            // During particle initialization
            var windFrequency = RaymathF.NextFloat(_data.WindData.MinWindFrequency, _data.WindData.MaxWindFrequency); // Adjust these ranges as needed
            var windAmplitude = RaymathF.NextFloat(_data.WindData.MinWindAmplitude, _data.WindData.MaxWindAmplitude);

// During particle update
            float windOscillation = MathF.Sin(particle.LifespanLeft * windFrequency) * windAmplitude;
            particle.Direction += new Vector2(windOscillation, 0) * dt;
            particle.Position += particle.Direction * particle.Speed * dt;
        }
    }
    
    public static Color Lerp(Color color1, Color color2, float t)
    {
        t = Math.Max(0, Math.Min(1, t));
        byte r = (byte)(color1.R + (color2.R - color1.R) * t);
        byte g = (byte)(color1.G + (color2.G - color1.G) * t);
        byte b = (byte)(color1.B + (color2.B - color1.B) * t);
        byte a = (byte)(color1.A + (color2.A - color1.A) * t);

        return new Color(r, g, b, a);
    }

    private void AddParticle(Vector2 pos)
    {
        
        pos += new Vector2(RaymathF.NextFloat(-_data.NoiseData.MinNoise, _data.NoiseData.MaxNoise), RaymathF.NextFloat(-_data.NoiseData.MinNoise, _data.NoiseData.MaxNoise));
        var p = new Particle();
        p.Lifespan = RaymathF.NextFloat(_data.LifeData.LifespanMin, _data.LifeData.LifespanMax);
        p.LifespanLeft = p.Lifespan;
        p.LifespanAmount = 1f;
        p.Position = pos;
        p.RotationSpeed = RaymathF.NextFloat(_data.RotationData.MinRotationSpeed, _data.RotationData.MaxRotationSpeed);
        p.Color = _data.ColorData.ColorStart;
        p.StartingScale = RaymathF.NextFloat(_data.SizeData.MinSizeStart, _data.SizeData.MaxSizeStart);
        p.Speed = RaymathF.NextFloat(_data.SpeedData.SpeedMin, _data.SpeedData.SpeedMax);
        if (p.Speed != 0)
        {
            var angle = RaymathF.NextFloat(_data.AngleData.Angle - _data.AngleData.AngleVariance, _data.AngleData.Angle + _data.AngleData.AngleVariance);
            angle = RaymathF.ToRadians(angle);
            p.Direction = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }
        else
        {
            p.Direction = Vector2.Zero;
        }
        _particles.Add(p);
        Count++;
    }

    public override void Draw()
    {
        foreach (var particle in _particles)
        {
            Raylib.DrawTextureEx(_data.Texture, particle.Position,  particle.Rotation , particle.Scale, particle.Color);
        }
    }
}