using System.Numerics;
using Core.cam2D;
using Raylib_cs;

namespace Core.modules.particles_system;

public class IWorldMouseEmitter : IParticleEmitter
{
    public Cam2D _camera2D;

    public IWorldMouseEmitter(Cam2D camera2D)
    {
        _camera2D = camera2D;
    }
    
    public Vector2 Position => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _camera2D.GetCamera2D());
}