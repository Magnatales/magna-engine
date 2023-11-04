using System.Reflection;
using Raylib_cs;

namespace Core;

public struct GameSettings
{
    public string Title;
    public int Width;
    public int Height;
    public string IconPath;
    public string LogDirectory;
    public int TargetFps;
    public int FixedTimeStep;
    public ConfigFlags WindowFlags;
    public Color BackgroundColor;
    
    public GameSettings()
    {
        Title = Assembly.GetEntryAssembly()?.GetName().Name ?? "Magna-Engine";
        Width = 1280;
        Height = 720;
        IconPath = string.Empty;
        LogDirectory = "logs";
        TargetFps = 80;
        FixedTimeStep = 60;
        WindowFlags =  ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE;
        BackgroundColor = Color.SKYBLUE;
    }
}