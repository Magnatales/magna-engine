using Core;
using Actors;
using Raylib_cs;

namespace Components;

public class DestroyAfterSeconds : Component
{
    private Actor _actor;
    private Scene _world;
    private float _duration;
    private float _timer;

    public DestroyAfterSeconds(Actor actor, Scene world, float duration)
    {
        _actor = actor;
        _world = world;
        _duration = duration;
        _timer = 0f;
    }

    public override void Update()
    {
        _timer += Raylib.GetFrameTime();

        if (_timer >= _duration)
        {
            _world.RemoveActor(_actor);
        }
    }
}