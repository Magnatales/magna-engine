using System.Numerics;
using Actors;
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
    
    public static void MoveTo(this Actor current, Vector2 target, float speed)
    {
        var direction = target - current.Position;
        var distance = direction.Length();

        if (distance <= speed ||
            distance == 0) return;

        var position = current.Position + direction / distance * speed;
        current.Position = position;
    }
    
    public static void MoveTo(this Actor current, Vector2 target, float speed, float minSeparationDistance, List<Actor> allActors)
    {
        var direction = target - current.Position;
        var distance = direction.Length();

        if (distance <= speed || distance == 0)
        {
            return; // We've reached the target or the target is at the same position
        }

        // Calculate the position after moving with speed
        var newPosition = current.Position + direction / distance * speed;

        // Check if the new position is too close to the target
        if (Vector2.Distance(newPosition, target) < minSeparationDistance)
        {
            // If it's too close to the target, don't move further
            return;
        }

        // Check if the new position is too close to other actors
        foreach (var other in allActors)
        {
            if (current != other && Vector2.Distance(newPosition, other.Position) < minSeparationDistance)
            {
                // If it's too close to any other actor, don't move further
                return;
            }
        }

        // Update the position
        current.Position = newPosition;
    }
}