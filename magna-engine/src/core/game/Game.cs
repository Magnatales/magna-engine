using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using ImGuiNET;
using Log;
using Raylib_cs;
using rlImGui_cs;

namespace Core;

public class Game
{
    private static readonly Version? _version = Assembly.GetExecutingAssembly().GetName().Version;
    private readonly double _fixedTimeStep;
    private double _timer;
    private readonly GameSettings _settings;
    private Image Logo { get; set; }
    public static event Action OnChangeAnimation;

    protected Game(GameSettings settings)
    {
        _settings = settings;
        _fixedTimeStep = 1.0 / settings.FixedTimeStep;
    }
    
    public void Run(Scene? scene)
    {
        Initialize();
        InitializeImGui();
        Logger.Debug("Initialize default scene...");
        SceneManager.SetScene(scene);
        Logger.Debug("Loop running...");
        while (!Window.ShouldClose())
        {
            Time.FrameCount++;
            Update(Time.Delta);
            AfterUpdate(Time.Delta);
            // for (_timer += Time.Delta; _timer >= _fixedTimeStep; _timer -= _fixedTimeStep)
            //     FixedUpdate();
            
            Graphics.BeginDrawing();
            Graphics.ClearBackground(_settings.BackgroundColor);
            Draw();
            Graphics.EndDrawing();
        }

        OnClose();
    }

    private void InitializeImGui()
    {
        //We need to use rlImGui to setup ImGui
        rlImGui.Setup(true, true);
        var style = ImGui.GetStyle();
        style.FrameRounding = 4f;
        style.WindowPadding = new Vector2(20, 20);
        style.ItemSpacing = new Vector2(20, 7);
        style.ChildRounding = 10f;
        var io = ImGui.GetIO();
        ImGui.StyleColorsClassic();
        io.Fonts.Clear();
        //Default font
        io.Fonts.AddFontFromFileTTF("resources/fonts/sourcecode/SourceCodePro-Medium.ttf", 20f);
        io.Fonts.Build();
        rlImGui.ReloadFonts();
    }

    private void Initialize()
    {
        if (_settings.LogDirectory != string.Empty)
            Logger.CreateLogFile(_settings.LogDirectory);
        var interpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
        interpolatedStringHandler.AppendLiteral("Magna Engine [");
        interpolatedStringHandler.AppendFormatted(_version);
        interpolatedStringHandler.AppendLiteral("] start...");
        interpolatedStringHandler.AppendLiteral("Initialize Logger...");
        Logger.Init(new Vector2(10f, 10f));
        Logger.Info(interpolatedStringHandler.ToStringAndClear());
        Logger.Info("\tCPU: " + SystemInfo.Cpu);
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
        interpolatedStringHandler.AppendLiteral("\tMEMORY: ");
        interpolatedStringHandler.AppendFormatted(SystemInfo.MemorySize);
        interpolatedStringHandler.AppendLiteral(" GB");
        Logger.Info(interpolatedStringHandler.ToStringAndClear());
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        interpolatedStringHandler.AppendLiteral("\tTHREADS: ");
        interpolatedStringHandler.AppendFormatted(SystemInfo.Threads);
        Logger.Info(interpolatedStringHandler.ToStringAndClear());
        Logger.Info("\tOS: " + SystemInfo.Os);
        Logger.Debug("Initialize Raylib logger...");
        Logger.SetupRaylibLogger();
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
        interpolatedStringHandler.AppendLiteral("Setting target fps to: ");
        interpolatedStringHandler.AppendFormatted(_settings.TargetFps > 0 ? _settings.TargetFps : "unlimited");
        Logger.Debug(interpolatedStringHandler.ToStringAndClear());
        Raylib.SetTargetFPS(_settings.TargetFps);
        Logger.Debug("Initialize window...");
        Window.SetConfigFlags(_settings.WindowFlags);
        Window.Init(_settings.Width, _settings.Height, _settings.Title);
        Logo = Raylib.LoadImage(_settings.IconPath);
        Window.SetIcon(Logo);
        Logger.Debug("Load content...");
        Load();
    }

    protected virtual void Load()
    {
        Fonts.Default = Raylib.LoadFontEx("resources/fonts/sourcecode/SourceCodePro-Medium.ttf", 20, null, 0);
        Fonts.DefaultBold = Raylib.LoadFontEx("resources/fonts/sourcecode/SourceCodePro-Bold.ttf", 24, null, 0);
    }

    protected virtual void Update(float dt)
    {
        SceneManager.Update(dt);
        //GuiManager.Update();
    }
    
    protected virtual void AfterUpdate(float dt)
    {
        SceneManager.AfterUpdate(dt);
        //GuiManager.AfterUpdate();
    }
    
    protected virtual void FixedUpdate()
    {
        SceneManager.FixedUpdate();
        //GuiManager.FixedUpdate();
    }
    
    protected virtual void Draw()
    {
        SceneManager.Draw();
        //GuiManager.Draw();
        Logger.Draw();
    }
    
    protected virtual void OnClose()
    {
        Logger.Warn("Application shuts down!");
    }
    
    protected void Dispose()
    {
        if (_settings.IconPath == string.Empty)
            Raylib.UnloadImage(Logo);
        //disposable.Dispose();
        Window.Close();
        //AudioDevice.Close();
        //GuiManager.ActiveGui?.Dispose();
        SceneManager.ActiveScene?.Dispose();
    }
}