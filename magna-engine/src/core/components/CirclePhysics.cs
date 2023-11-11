using System.Numerics;
using Actors;
using Helpers.quadtree;
using Raylib_cs;

namespace Components;

public class CirclePhysics : Component
{
    private readonly Actor _actor;
    private readonly float _radius;
    private bool _isColliding;
    private readonly Quadtree _quadtree;
    private readonly List<Actor> _actors;
    private readonly bool _useQuadtree;
    private readonly Random random = new();

    public CirclePhysics(Actor actor, float radius, Quadtree quadtree, List<Actor> actors,
        bool useQuadtree)
    {
        _actor = actor;
        _radius = radius;
        _quadtree = quadtree;
        _actors = actors;
        _useQuadtree = useQuadtree;
    }

    public override void Draw()
    {
        var positionWithOffset = _actor.Position;
        Raylib.DrawCircle((int)positionWithOffset.X, (int)positionWithOffset.Y, _radius, _isColliding ? Color.RED : Color.GREEN);
    }
    
    public override void Update()
    {
        var maxSpeed = 100;

        var frameTime = Raylib.GetFrameTime();

        var speed = maxSpeed * frameTime;

        var angle = (float)random.NextDouble() * 2 * MathF.PI;
        
        var xOffset = speed * MathF.Cos(angle);
        var yOffset = speed * MathF.Sin(angle);

        var newPosition = new Vector2(
            _actor.Position.X + xOffset,
            _actor.Position.Y + yOffset
        );
        _actor.Position = newPosition;
    }

    public void SetCollision(bool value)
    {
        _isColliding = value;
    }
}