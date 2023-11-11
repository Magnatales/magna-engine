using System.Numerics;
using Raylib_cs;

namespace Core.modules.particles_system;

public class ILineEmitter : IParticleEmitter
{
    public Vector2 StartPoint { get; }
    public Vector2 EndPoint { get; }

    public ILineEmitter(Vector2 startPoint, Vector2 endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public Vector2 GetRandomPositionAlongLine()
    {
        float t = Raylib.GetRandomValue(0, 100) / 100.0f; // Random point along the line
        return new Vector2(
            Raymath.Lerp(StartPoint.X, EndPoint.X, t),
            Raymath.Lerp(StartPoint.Y, EndPoint.Y, t)
        );
    }

    // Implement the IParticleEmitter interface
    public Vector2 Position => GetRandomPositionAlongLine();
}