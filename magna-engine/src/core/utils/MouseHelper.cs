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
        var mousePositionText = "Screen x:" + GetMouseX() + " y:" + GetMouseY();
        var mousePosition = new Vector2(GetMouseX() - 75f, GetMouseY() -22f);
        RectangleUtils.DrawRoundedRectangleWithText(mousePositionText, mousePosition, 200, 27, 0.5f, rectColor, Fonts.Default, Color.BLACK);
    }
    
    public static void DrawMouseWorldPosition(Camera2D camera2D)
    {
        var rectColor = new Color(255, 255, 255, 200);

        // Get the mouse position in screen coordinates
        var mouseX = GetMouseX();
        var mouseY = GetMouseY();

        // Convert screen coordinates to world coordinates
        var mousePositionWorld = Raylib.GetScreenToWorld2D(new Vector2(mouseX, mouseY), camera2D);

        var mousePositionText = "World x:" + (int)mousePositionWorld.X + " y:" + (int)mousePositionWorld.Y;
        var mousePosition = new Vector2(mouseX - 75f, mouseY - 22f);

        RectangleUtils.DrawRoundedRectangleWithText(mousePositionText, mousePosition, 200, 27, 0.5f, rectColor, Fonts.Default, Color.BLACK);
    }
}