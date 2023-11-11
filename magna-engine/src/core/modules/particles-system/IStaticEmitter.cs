using System.Numerics;

namespace Core.modules.particles_system;

public class IStaticEmitter : IParticleEmitter
{
    public IStaticEmitter(Vector2 position)
    {
        Position = position;
    }
    public Vector2 Position { get; }
}