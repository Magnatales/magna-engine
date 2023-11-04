using Entities;

namespace Components;

public abstract class Component
{
    public virtual void Update() {}
    public virtual void AfterUpdate(){}
    public virtual void FixedUpdate(){}
    public virtual void Draw() {}
    public virtual void DrawDebug(){}
}