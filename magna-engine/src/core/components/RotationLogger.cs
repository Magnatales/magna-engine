using System.Numerics;
using Entities;
using Raylib_cs;

namespace Components;

public class RotationLogger : Component
{
    private readonly Actor _owner;

    public RotationLogger(Actor owner)
    {
        _owner = owner;
    }
    
    public override void DrawDebug()
    {
        Raylib.DrawTextEx(Fonts.Default, "Rot " + _owner.Rotation.ToString("F0"), _owner.Position + new Vector2(-25, -100),
                Fonts.Default.BaseSize, 2, Color.BLACK);
    }
}