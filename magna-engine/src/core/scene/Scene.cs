using System.Numerics;
using Core.cam2D;
using Entities;
using Game.UI;

namespace Core;

public abstract class Scene : IDisposable
{
    public readonly string Name;
    private readonly Dictionary<string, Actor> _entities;
    private readonly Dictionary<string, Widget> _widgets;
    private readonly Batching _batching;
    protected readonly Cam2D Cam2D;

    protected Scene(string name)
    {
        Name = name;
        _entities = new Dictionary<string, Actor>();
        _widgets = new();
        Cam2D = new Cam2D(Vector2.Zero, Cam2D.CameraFollowMode.Normal, 1f);
        _batching = new Batching();
    }

    public virtual void Init()
    {
        
    }
    
    protected internal virtual void Update(float dt)
    {
        Cam2D?.Update();
        foreach (var entity in _entities.Values)
        {
            if(Cam2D?.IsActorInsideCameraBounds(entity) ?? true)
                entity.Update(dt);
        }
        
        foreach (var widget in _widgets.Values)
        {
            if(widget.IsVisible)
                widget.Update(dt);
        }
    }
    
    protected internal virtual void AfterUpdate(float dt)
    {
        foreach (var entity in _entities.Values)
            entity.AfterUpdate(dt);
    }

    protected internal virtual void FixedUpdate()
    {
        foreach (var entity in _entities.Values)
            entity.FixedUpdate();
    }

    protected internal virtual void Draw()
    {
        _batching.RenderBatches(Cam2D);
    }
    
    protected internal virtual void DrawWidgets()
    {
        foreach (var widget in _widgets.Values)
        {
            if(widget.IsVisible)
                widget.Draw();
        }
    }
    
    public void AddActor(Actor actor)
    {
        _entities.Add(actor.Id, actor);
        _batching.AddToBatch(actor);
    }
    
    public void AddActor(Actor actor, string id)
    {
        _entities.Add(id, actor);
        _batching.AddToBatch(actor);
    }
    
    public void RemoveActor(string id)
    {
        _entities[id].Dispose();
        _entities.Remove(id);
        _batching.RemoveFromBatch(_entities[id]);
    }
    
    public void RemoveActor(Actor entity)
    {
        RemoveActor(entity.Id);
    }
    
    public Actor GetActor(string id)
    {
        return _entities[id];
    }

    public Actor[] GetActors()
    {
        return _entities.Values.ToArray();
    }
    
    public void AddWidget(Widget widget, bool visible = true)
    {
        if(visible)
            widget.Show();
        
        _widgets.Add(widget.Id, widget);
    }
    
    public void RemoveWidget(string id)
    {
        _widgets.Remove(id);
    }
    
    public void RemoveWidget(Widget widget)
    {
        RemoveWidget(widget.Id);
    }

    public void Dispose()
    {
        foreach (var disposable in _entities.Values)
            disposable.Dispose();
        _entities.Clear();
    }
}