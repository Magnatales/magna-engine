using System.Numerics;
using Raylib_cs;

namespace Helpers;

public static class MouseHelper
{
    public static float GetMouseX()
    {
        return Raylib.GetMousePosition().X;
    }
    
    public static int GetMouseXInt()
    {
        return (int)Raylib.GetMousePosition().X;
    }
    
    public static float GetMouseY()
    {
        return Raylib.GetMousePosition().Y;
    }
    
    public static int GetMouseYInt()
    {
        return (int)Raylib.GetMousePosition().Y;
    }

    public static void DrawMousePosition()
    {
        var rectColor = new Color(255, 255, 255, 200);
        var mousePositionText = "x:" + GetMouseX() + " y:" + GetMouseY();
        var mousePosition = new Vector2(GetMouseX() - 75f, GetMouseY() -22f);
        RectangleUtils.DrawRoundedRectangleWithText(mousePositionText, mousePosition, 150, 27, 0.5f, rectColor, Fonts.Default, Color.BLACK);
    }
}