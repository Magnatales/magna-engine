using System.Numerics;
using Raylib_cs;

namespace Extensions;

public static class ShapeExtensions
{
    public static void UpdateSizeFromCenter(this ref Rectangle originalRect, Vector2 targetSize)
    {
        var deltaX = (targetSize.X - originalRect.Width) / 2;
        var deltaY = (targetSize.Y - originalRect.Height) / 2;
        originalRect = new Rectangle(originalRect.X - deltaX, originalRect.Y - deltaY, targetSize.X, targetSize.Y);
    }

    public static bool ContainsPoint(this Rectangle rectangle, Vector2 point)
    {
        return point.X >= rectangle.X && point.X <= rectangle.X + rectangle.Width &&
               point.Y >= rectangle.Y && point.Y <= rectangle.Y + rectangle.Height;
    }

    public static bool Intersects(this Rectangle rectA, Rectangle rectB)
    {
        return !(rectA.X + rectA.Width < rectB.X ||
                 rectA.X > rectB.X + rectB.Width ||
                 rectA.Y + rectA.Height < rectB.Y ||
                 rectA.Y > rectB.Y + rectB.Height);
    }

    public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
    {
        return LineIntersectsLine(p1, p2, new Vector2((int)r.X, (int)r.Y),
                   new Vector2((int)(r.X + r.Width), (int)r.Y)) ||
               LineIntersectsLine(p1, p2, new Vector2((int)(r.X + r.Width), (int)r.Y),
                   new Vector2((int)(r.X + r.Width), (int)(r.Y + r.Height))) ||
               LineIntersectsLine(p1, p2, new Vector2((int)(r.X + r.Width), (int)(r.Y + r.Height)),
                   new Vector2((int)r.X, (int)(r.Y + r.Height))) ||
               LineIntersectsLine(p1, p2, new Vector2((int)r.X, (int)(r.Y + r.Height)),
                   new Vector2((int)r.X, (int)r.Y)) ||
               (r.ContainsPoint(p1) && r.ContainsPoint(p2));
    }

    private static bool LineIntersectsLine(Vector2 l1P1, Vector2 l1P2, Vector2 l2P1, Vector2 l2P2)
    {
        var q = (l1P1.Y - l2P1.Y) * (l2P2.X - l2P1.X) - (l1P1.X - l2P1.X) * (l2P2.Y - l2P1.Y);
        var d = (l1P2.X - l1P1.X) * (l2P2.Y - l2P1.Y) - (l1P2.Y - l1P1.Y) * (l2P2.X - l2P1.X);

        if (d == 0) return false;

        var r = q / d;

        q = (l1P1.Y - l2P1.Y) * (l1P2.X - l1P1.X) - (l1P1.X - l2P1.X) * (l1P2.Y - l1P1.Y);
        var s = q / d;

        if (r < 0 || r > 1 || s < 0 || s > 1) return false;

        return true;
    }
}