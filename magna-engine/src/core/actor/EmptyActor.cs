using System.Numerics;

namespace Entities;

public class EmptyActor : Actor
{
    public override string Id => "Actor";
    
    public EmptyActor(Vector2 worldPosition) : base(worldPosition)
    {
        
    }

    
}