using System.Numerics;
using Entities;
using Raylib_cs;

namespace Extensions;

public static class ActorExtensions
{
    public static float GetX(this Actor actor)
    {
        return actor.Position.X;
    }
    
    public static int GetXInt(this Actor actor)
    {
        return (int)actor.Position.X;
    }
    
    public static float GetY(this Actor actor)
    {
        return actor.Position.Y;
    }
    
    public static int GetYInt(this Actor actor)
    {
        return (int)actor.Position.Y;
    }
}