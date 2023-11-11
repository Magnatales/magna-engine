using System.Numerics;
using Raylib_cs;

namespace Core.modules.particles_system;

/// <summary>
///     A struct representing a single particle in a particle system
/// </summary>
public class Particle
{
    public Vector2 Position;
    public float LifespanLeft;
    public float Lifespan;
    public float LifespanAmount;
    public Color Color;
    public float StartingScale;
    public float Scale;
    public Vector2 Direction;
    public float Rotation;
    public float RotationSpeed;
    public float Speed;

}