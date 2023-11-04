using System.Numerics;
using Raylib_cs;

namespace Helpers;

public static class ScreenDebuggers
{
    public static void DrawFps(Font font, Color color)
    {
        // Define the position and size of the rounded rectangle
        Rectangle roundedRect = new Rectangle(
            Raylib.GetScreenWidth() - 120, 7.5f, 116, 25
        );

        // Define the roundness and number of segments for the rounded rectangle
        float roundness = 0.5f;
        int segments = 0; // You can adjust this value as needed

        // Draw the rounded rectangle
        Raylib.DrawRectangleRounded(roundedRect, roundness, segments, new Color(255, 255, 255, 200));

        // Draw the FPS text
        Raylib.DrawTextEx(font, "FPS: " + Raylib.GetFPS(), new Vector2(Raylib.GetScreenWidth() - 100, 10), font.BaseSize, 2, color);
    }
}