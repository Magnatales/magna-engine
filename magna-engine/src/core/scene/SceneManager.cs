namespace Core;

public static class SceneManager
{
    public static Scene? ActiveScene { get; private set; }
    
    internal static void Update(float dt) => ActiveScene?.Update(dt);
    
    internal static void AfterUpdate(float dt) => ActiveScene?.AfterUpdate(dt);
    
    internal static void FixedUpdate() => ActiveScene?.FixedUpdate();
    
    internal static void Draw() => ActiveScene?.Draw();
    public static void SetScene(Scene? scene)
    {
        ActiveScene?.Dispose();
        ActiveScene = scene;
        ActiveScene?.Init();
    }
}