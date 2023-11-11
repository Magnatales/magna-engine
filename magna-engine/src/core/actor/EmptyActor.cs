using System.Numerics;

namespace Actors;

public class EmptyActor : Actor
{
    public EmptyActor(Vector2 worldPosition) : base(worldPosition)
    {
        Id = "Actor";
    }
}