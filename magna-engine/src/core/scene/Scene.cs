using System.Numerics;
using Core.cam2D;
using Actors;
using Game.UI;

namespace Core;

public abstract class Scene : IDisposable
{
    public readonly string Name;
    protected Dictionary<string, Actor> _actors;
    private readonly Dictionary<string, Widget> _widgets;
    //private readonly Batching _batching;
    protected readonly Cam2D Cam2D;

    protected Scene(string name)
    {
        Name = name;
        _actors = new Dictionary<string, Actor>();
        _widgets = new();
        Cam2D = new Cam2D(new Vector2(200, 200), Cam2D.CameraFollowMode.Smooth, 2f);
       // _batching = new Batching();
    }

    public virtual void Init()
    {
        
    }
    
    protected internal virtual void Update(float dt)
    {
        // Cam2D?.Update();
        // //_actors = _actors.Values.OrderBy(actor => actor.Position.Y).ToDictionary(x => x.Id, x => x);
        // foreach (var entity in _actors.Values)
        // {
        //     if(Cam2D?.IsActorInsideCameraBounds(entity) ?? true)
        //         entity.Update(dt);
        // }
        //
        // foreach (var widget in _widgets.Values)
        // {
        //     if(widget.IsVisible)
        //         widget.Update(dt);
        // }
    }
    
    protected internal virtual void AfterUpdate(float dt)
    {
        foreach (var entity in _actors.Values)
            entity.AfterUpdate(dt);
    }

    protected internal virtual void FixedUpdate()
    {
        foreach (var entity in _actors.Values)
            entity.FixedUpdate();
    }

    protected internal virtual void Draw()
    {
        // foreach (var actorsValue in _actors.Values)
        // {
        //     if(Cam2D?.IsActorInsideCameraBounds(actorsValue) ?? true)
        //         actorsValue.Draw();
        // }
        // foreach (var widget in _actors.Values)
        // {
        //     widget.Draw();
        // }
        //_batching.RenderBatches(Cam2D);
    }
    
    protected virtual void DrawWidgets()
    {
        foreach (var widget in _widgets.Values)
        {
            if(widget.IsVisible)
                widget.Draw();
        }
    }

    protected void AddActor(Actor actor)
    {
        _actors.Add(actor.Id, actor);
        //_batching.AddToBatch(actor);
    }

    protected void AddActorAndOverrideId(Actor actor, string id)
    {
        actor.Id = id;
        _actors.Add(id, actor);
        //_batching.AddToBatch(actor);
    }
    
    public void RemoveActor(string id)
    {
        _actors[id].Dispose();
        _actors.Remove(id);
        //_batching.RemoveFromBatch(_entities[id]);
    }
    
    public void RemoveActor(Actor entity)
    {
        RemoveActor(entity.Id);
    }

    public Actor GetActor(string id)
    {
        return _actors[id];
    }

    public Actor[] GetActors()
    {
        return _actors.Values.ToArray();
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

    public virtual void Dispose()
    {
        foreach (var disposable in _actors.Values)
            disposable.Dispose();
        _actors.Clear();
    }
}