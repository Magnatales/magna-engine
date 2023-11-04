using System.Numerics;
using Raylib_cs;

namespace Helpers;

public static class RectangleUtils
{
    public static void DrawRoundedRectangleWithText(string text, Vector2 position, float width, float height,
        float roundness, Color rectColor, Font font, Color textColor)
    {
        // Get the screen width and height
        var screenWidth = Raylib.GetScreenWidth();
        var screenHeight = Raylib.GetScreenHeight();

        // Adjust the position of the rectangle to stay within the screen boundaries
        if (position.X < 0) position.X = 0;
        if (position.X + width > screenWidth) position.X = screenWidth - width;
        if (position.Y < 0) position.Y = 0;
        if (position.Y + height > screenHeight) position.Y = screenHeight - height;

        // Define the rounded rectangle
        var roundedRect = new Rectangle(position.X, position.Y, width, height);

        // Draw the rounded rectangle
        Raylib.DrawRectangleRounded(roundedRect, roundness, 0, rectColor);

        // Adjust the text's position to stay within the screen boundaries
        var textX = position.X + 5;
        var textY = position.Y + 3;

        if (textX < 0) textX = 0;
        if (textX > screenWidth - 140) textX = screenWidth - 140;
        if (textY < 0) textY = 0;
        if (textY > screenHeight - 20) textY = screenHeight - 20;

        // Draw the text with adjusted position
        Raylib.DrawTextEx(font, text, new Vector2(textX, textY), font.BaseSize, 2, textColor);
    }
    
    public static void DrawBorderedText(string text, Font font, Color textColor, Color borderColor, int borderWidth)
    {
        var textSize = Raylib.MeasureTextEx(font, text, font.BaseSize, 2);
        var textPosition = new Vector2(
            (Raylib.GetScreenWidth() - textSize.X) / 2,
            (Raylib.GetScreenHeight() - textSize.Y) / 2
        );
        
        Raylib.DrawTextEx(font, text, new Vector2(textPosition.X - borderWidth, textPosition.Y), font.BaseSize, 2, borderColor);
        Raylib.DrawTextEx(font, text, new Vector2(textPosition.X + borderWidth, textPosition.Y), font.BaseSize, 2, borderColor);
        Raylib.DrawTextEx(font, text, new Vector2(textPosition.X, textPosition.Y - borderWidth), font.BaseSize, 2, borderColor);
        Raylib.DrawTextEx(font, text, new Vector2(textPosition.X, textPosition.Y + borderWidth), font.BaseSize, 2, borderColor);
        Raylib.DrawTextEx(font, text, textPosition, font.BaseSize, 2, textColor);
    }

    public static Rectangle CreateCentered(Vector2 center, float width, float height)
    {
        return new Rectangle(center.X - width / 2, center.Y - height / 2, width, height);
    }
}