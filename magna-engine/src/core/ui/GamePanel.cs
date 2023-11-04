using System.Numerics;
using ImGuiNET;
using Log;
using Core.Tweening;
using Raylib_cs;
using rlImGui_cs;

namespace Game.UI;

public class GamePanel : Widget
{
    public override string Id => "GamePanel";
    private readonly List<Widget> _buttons = new();
    private Widget _closeButton;
    private Tweener _tweener;
    public Vector2 _windowPosition;
    public GamePanel()
    {
        _buttons.Add(new Button( "Brutal", () => Logger.ScreenLog("Brutal! clicked", Logger.Colors.Success), new Vector2(60, 30)));
        _buttons.Add(new Button( "Awesome", () => Logger.ScreenLog("Awesome! clicked", Logger.Colors.Success), new Vector2(75, 30)));
        _buttons.Add(new Button( "Epic", () => Logger.ScreenLog("Epic! clicked", Logger.Colors.Success), new Vector2(60, 30)));
        _closeButton = new Button( "X", Hide, new Vector2(25,25));
        
        _windowPosition = new Vector2()
        {
            X = -500,
            Y = Raylib.GetScreenHeight()/2f
        };
        _tweener = new Tweener();
    }

    public override void Show()
    {
        base.Show();
        _tweener.TweenTo(target: this,  expression: panel => _windowPosition,
            toValue: new Vector2(20, Raylib.GetScreenHeight() / 2f), 0.5f, 1f).Easing(Ease.Back.Out);
    }
    public override void Hide()
    {
        _tweener.TweenTo(target: this, expression: panel => _windowPosition,
            toValue: new Vector2(-500, Raylib.GetScreenHeight() / 2f), 0.5f).Easing(Ease.Back.In).OnEnd(_ => base.Hide());
        Logger.ScreenLog("GamePanel is hidden", Logger.Colors.Error);
    }
    
    public override void Update(float dt)
    {
        base.Update(dt);
        _tweener?.Update(dt);
    }

    public override void Draw()
    {
        base.Draw();
        rlImGui.Begin();
        ImGui.Begin("Title!",ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoScrollbar);
        ImGui.SetWindowPos(new Vector2(_windowPosition.X, _windowPosition.Y));
        ImGui.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0, 0, 0, 0.75f));
        ImGui.BeginChild("Buttons", new Vector2(280, 95), true);
        ImGui.SetCursorPos(new Vector2(240, 5));
        _closeButton.Draw();
        foreach (var button in _buttons)
        {
            button.Draw();
            ImGui.SameLine();
        }
        ImGui.ShowDemoWindow();
        ImGui.EndChild();
        if (rlImGui.ImageButton("Hey", Textures.GetScarfy()))
        {
        }

        ImGui.End();
        rlImGui.End();
    }
}