using System.Numerics;
using System.Timers;
using Components;
using Log;
using Raylib_cs;

namespace Entities;

public abstract class Actor : IDisposable
{
    public abstract string Id { get; }
    public int Layer = 0;
    private readonly List<Component> _components = new();
    
    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            //UpdateChildTransforms();
        }
    }
    
    private float _rotation;
    public float Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;
            //UpdateChildTransforms();
        }
    }

    private Vector2 _scale;
    public Vector2 Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            //UpdateChildTransforms();
        }
    }
    public List<Actor> Children = new List<Actor>();
    public Actor? Parent { get; private set; }

    public Actor(Vector2 worldPosition)
    {
        Position = worldPosition;
        Scale = Vector2.One;
        Parent = null;
    }
    
    public void SetParent(Actor parent)
    {
        if (parent == this)
        {
            Logger.Debug("Cannot set parent to self!");
            return;
        }
        if (parent.Children.Contains(this))
        {
            Logger.Debug("Entity already exists as child!");
            return;
        }

        Parent?.Children.Remove(this);
        Parent = parent;
        Parent.Children.Add(this);
    }
    
    public void RemoveParent()
    {
        Parent?.Children.Remove(this);
        Parent = null;
    }
    
    public void AddChild(Actor child)
    {
        if (child == this)
        {
            Logger.Debug("Cannot add self as child!");
            return;
        }
        if (Children.Contains(child))
        {
            Logger.Debug("Entity already exists as child!");
            return;
        }

        child.Parent?.Children.Remove(child);
        child.Parent = this;
        Children.Add(child);
    }
    
    public void RemoveChild(Actor child)
    {
        if (Children.Contains(child))
            Children.Remove(child);
        else
            Logger.Debug("Entity does not exist as child!");
    }

    public T AddComponent<T>(T component) where T : Component
    {
        var existingComponent = _components.OfType<T>().FirstOrDefault();
        if (existingComponent != null)
        {
            Logger.Debug("Component already exists on entity!");
            return existingComponent;
        }
        _components.Add(component);
        return component;
    }

    public bool TryGetComponent<T>(out T? component) where T : Component
    {
        foreach (var c in _components)
        {
            if (c is T tComponent)
            {
                component = tComponent;
                return true;
            }
        }

        component = null;
        return false;
    }

    protected void RemoveComponent(Component component)
    {
        if(_components.Contains(component))
            _components.Remove(component);
        else
            Logger.Debug("Component does not exist on entity!");
    }

    public virtual void Update(float dt)
    {
        for (var index = 0; index < _components.Count; index++)
        {
            var component = _components[index];
            component.Update();
        }
    }
    
    public virtual void AfterUpdate(float deltaTime)
    {
        for (var index = 0; index < _components.Count; index++)
        {
            var component = _components[index];
            component.Update();
        }
    }
    
    public virtual void FixedUpdate()
    {
        for (var index = 0; index < _components.Count; index++)
        {
            var component = _components[index];
            component.Update();
        }
    }

    public virtual void Draw()
    {
        for (var index = 0; index < _components.Count; index++)
        {
            var component = _components[index];
            component.Draw();
        }

        for (var index = 0; index < Children.Count; index++)
        {
            var child = Children[index];
            child.Draw();
        }
    }

    public virtual void DrawDebug()
    {
        for (var index = 0; index < _components.Count; index++)
        {
            var component = _components[index];
            component.DrawDebug();
        }
    }

    public void Dispose()
    {
        Children.Clear();
        _components.Clear();
    }
}