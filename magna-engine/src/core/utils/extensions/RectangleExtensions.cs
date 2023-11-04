using System.Numerics;
using Components;
using Raylib_cs;

namespace Extensions;

public static class RectangleExtensions
{
    public static void UpdateSizeFromCenter(this ref Rectangle originalRect, Vector2 targetSize)
    {
        var deltaX = (targetSize.X - originalRect.Width) / 2;
        var deltaY = (targetSize.Y - originalRect.Height) / 2;
        originalRect = new Rectangle(originalRect.X - deltaX, originalRect.Y - deltaY, targetSize.X, targetSize.Y);
    }
}