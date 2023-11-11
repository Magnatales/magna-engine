using System.Numerics;
using Raylib_cs;

namespace Core.modules.particles_system;

public class IMouseEmitter : IParticleEmitter
{
    public Vector2 Position => Raylib.GetMousePosition();

    public Vector2 Velocity => Raylib.GetMousePosition() - Position;
}