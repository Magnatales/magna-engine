using Core.cam2D;
using Actors;
using Motion;
using Raylib_cs;

public class Batching
{
    private readonly SortedDictionary<int, Dictionary<Shader, List<Actor>>> _layeredShaderBatches = new();
    private readonly List<Actor> _actorsWithoutShader = new();

    public void AddToBatch(Actor actor)
    {
        actor.TryGetComponent<Sprite2D>(out var sprite);
        var shader = sprite?.GetShader();

        if (shader != null)
        {
            var layer = actor.Layer;
            if (!_layeredShaderBatches.TryGetValue(layer, out var shaderBatch))
            {
                shaderBatch = new Dictionary<Shader, List<Actor>>();
                _layeredShaderBatches[layer] = shaderBatch;
            }

            if (!shaderBatch.ContainsKey(shader.Value))
            {
                shaderBatch[shader.Value] = new List<Actor>();
            }

            shaderBatch[shader.Value].Add(actor);
        }
        else
        {
            _actorsWithoutShader.Add(actor);
        }
    }

    public void RemoveFromBatch(Actor actor)
    {
        actor.TryGetComponent<Sprite2D>(out var spriteAnimator);
        var shader = spriteAnimator?.GetShader();

        var layer = actor.Layer;
        if (shader != null && _layeredShaderBatches.TryGetValue(layer, out var shaderBatch) && shaderBatch.ContainsKey(shader.Value))
        {
            shaderBatch[shader.Value].Remove(actor);
        }
        else
        {
            _actorsWithoutShader.Remove(actor);
        }
    }

    public void OnActorUpdatedShader(Actor entity)
    {
        RemoveFromBatch(entity);
        AddToBatch(entity);
    }

    public void OnActorUpdatedLayer(Actor entity)
    {
        RemoveFromBatch(entity);
        AddToBatch(entity);
    }

    public void RenderBatches(Cam2D cam2D)
    {
        foreach (var layer in _layeredShaderBatches.Keys)
        {
            foreach (var shaderBatch in _layeredShaderBatches[layer])
            {
                var actorsWithShader = shaderBatch.Value;
                var shader = shaderBatch.Key;

                Raylib.BeginShaderMode(shader);
                for (var index = actorsWithShader.Count - 1; index >= 0; index--)
                {
                    var actor = actorsWithShader[index];
                    if (cam2D?.IsActorInsideCameraBounds(actor) ?? true)
                        actor.Draw();
                }
                Raylib.EndShaderMode();
            }
        }

        for (var index = _actorsWithoutShader.Count - 1; index >= 0; index--)
        {
            var actor = _actorsWithoutShader[index];
            if (cam2D?.IsActorInsideCameraBounds(actor) ?? true)
                actor.Draw();
        }
    }
}
