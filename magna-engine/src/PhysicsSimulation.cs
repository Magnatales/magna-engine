using Actors;
using Components;
using Core;
using Helpers.quadtree;
using Raylib_cs;

public class PhysicsSimulation : IDisposable
{
    private readonly Quadtree _quadtree;

    public PhysicsSimulation(Quadtree quadtree)
    {
        _quadtree = quadtree;
        _quadtree.OnQuadTreeUpdate += OnQuadTreeUpdate;
    }

    private readonly List<Actor> _actorsInRange = new();
    private readonly Dictionary<Actor, HashSet<Actor>> _actorChecked = new();

    private void OnQuadTreeUpdate(List<Actor> actorsInViewport)
    {
        //_actorChecked.Clear();
        foreach (var actor in actorsInViewport)
        {
            actor.Update(Time.Delta);
            _actorsInRange.Clear();
            _quadtree.GetActorsWithinActorRange(actor, _actorsInRange);
            var collided = false;
            var actorPhysics = actor.GetComponent<CirclePhysics>();

            foreach (var actorInRange in _actorsInRange)
            {
                if (actor == actorInRange)
                    continue;
                
                if (Raylib.CheckCollisionCircles(actor.Position, 1.8f, actorInRange.Position, 1.8f))
                {
                    collided = true;
                }
            }
            actorPhysics.SetCollision(collided);
        }
    }

    public void Dispose()
    {
        _quadtree.OnQuadTreeUpdate -= OnQuadTreeUpdate;
    }
}