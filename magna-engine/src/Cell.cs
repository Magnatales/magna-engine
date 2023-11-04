using System.Numerics;
using Raylib_cs;

public class Cell
{
    public Vector2 Position;
    public Vector2 Size;
    public Color Color;
    public Cell(Vector2 position, Vector2 size, Color color)
    {
        Position = position;
        Size = size;
        Color = color;
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)Position.X-3000, (int)Position.Y-3000, (int)Size.X, (int)Size.Y, Color);
    }
}