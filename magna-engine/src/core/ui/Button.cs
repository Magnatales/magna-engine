using System.Numerics;
using ImGuiNET;
using Log;
using Raylib_cs;

namespace Game.UI;

public class Button : Widget
{
    public override string Id => "Button";
    private Vector2 _size;
    private string _text;
    private Action _callBack;
    private Vector2 _scaledSize;
    private Vector2 _originalSize;

    public Button(string text, Action callback, Vector2 size = default)
    {
        _size = size;
        _text = text;
        _callBack = callback;
        IsVisible = true;
        _originalSize = _size;
        _scaledSize = _size * 1.1f;
    }

    public override void Draw()
    {
        base.Draw();
        if (_size != Vector2.Zero)
        {
            
            var isHovered = ImGui.IsMouseHoveringRect(ImGui.GetCursorScreenPos(), ImGui.GetCursorScreenPos() + _size);
            if(isHovered)
                Logger.ScreenLog($"{_text} button is hovered");
            
            Vector2 adjustedSize = isHovered ? _size + new Vector2(5, 5) : _size;
            if (ImGui.Button(_text, adjustedSize))
            {
                _callBack?.Invoke();
            }
        }
        else
        {
            if (ImGui.Button(_text))
            {
                _callBack?.Invoke();
                
            }
        }
       
    }
}