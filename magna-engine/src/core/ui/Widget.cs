namespace Game.UI;

public abstract class Widget
{
    public abstract string Id { get; }
    public bool IsVisible { get; protected set; }
    
    public virtual void Show()
    {
        IsVisible = true;
    }

    public virtual void Hide()
    {
        IsVisible = false;
    }

    public virtual void Update(float dt)
    {
       
    }

    public virtual void Draw()
    {
        
    }

    public virtual void DrawDebug()
    {
        
    }
}