using System.Numerics;
using Actors;
using Raylib_cs;

namespace Core.cam2D;

public class Cam2D
{
    private Camera2D _camera2D;
    public Actor Target;
    public CameraFollowMode Mode;
    public float MinZoom;
    public float MaxZoom;
    public float ZoomSpeed;
    public float MinFollowSpeed;
    public float MinFollowEffectLength;
    public float FractionFollowSpeed;
    private Vector2 previousMousePosition;
    
    public Cam2D(Vector2 position, CameraFollowMode mode, float zoom = 5f)
    {
        _camera2D = new Camera2D();
        Position = position;
        Mode = mode;
        MinZoom = 0.065f;
        MaxZoom = 9f;
        Zoom = zoom;
        ZoomSpeed = 0.05f;
        MinFollowSpeed = 30f;
        MinFollowEffectLength = 10f;
        FractionFollowSpeed = 4f;
    }
    
    public Vector2 Position
    {
        get => _camera2D.Target;
        set => _camera2D.Target = value;
    }
    
    public float Rotation
    {
        get => _camera2D.Rotation;
        set => _camera2D.Rotation = value;
    }
    
    public Vector2 Offset
    {
        get => _camera2D.Offset;
        set => _camera2D.Offset = value;
    }
    
    public float Zoom
    {
        get => _camera2D.Zoom;
        set
        {
            if (value < (double)MinZoom)
                _camera2D.Zoom = MinZoom;
            else if (value > (double)MaxZoom)
                _camera2D.Zoom = MaxZoom;
            else
                _camera2D.Zoom = value;
        }
    }

    public void SetTarget(Actor target)
    {
        Position = target.Position;
        Target = target;
    }

    public void Update()
    {
        Zoom += Input.GetMouseWheelMove() * ZoomSpeed;
        if (Target == null) return;
        switch (Mode)
        {
            case CameraFollowMode.Normal:
                NormalMovement();
                break;
            case CameraFollowMode.Smooth:
                SmoothMovement();
                break;
        }
    }
    
    private void NormalMovement()
    {
        Offset = new Vector2(Window.GetScreenWidth() / 2f, Window.GetScreenHeight() / 2f);
        Position = Target.Position;
    }
    
    private void SmoothMovement()
    {
        Offset = new Vector2(Window.GetScreenWidth() / 2f, Window.GetScreenHeight() / 2f);
        var vector2 = Target.Position - Position;
        var num1 = vector2.Length();
        if (num1 <= (double)MinFollowEffectLength)
            return;
        var num2 = Math.Max(FractionFollowSpeed * num1, MinFollowSpeed);
        Position += vector2 * (num2 * Time.Delta / num1);
    }

    private static Vector2 offset = new Vector2(50, 50);
    public bool IsActorInsideCameraBounds(Actor actor)
    {
        var leftBoundary = _camera2D.Target.X - Raylib.GetScreenWidth() / (2f * _camera2D.Zoom) - offset.X;
        var rightBoundary = _camera2D.Target.X + Raylib.GetScreenWidth() / (2f * _camera2D.Zoom) + offset.X;
        var topBoundary = _camera2D.Target.Y - Raylib.GetScreenHeight() / (2f * _camera2D.Zoom) - offset.Y;
        var bottomBoundary = _camera2D.Target.Y + Raylib.GetScreenHeight() / (2f * _camera2D.Zoom) + offset.Y;

        return (actor.Position.X >= leftBoundary &&
                actor.Position.X <= rightBoundary &&
                actor.Position.Y >= topBoundary &&
                actor.Position.Y <= bottomBoundary);
    }

    public Matrix4x4 GetMatrix2D()
    {
        return Raylib.GetCameraMatrix2D(_camera2D);
    }
    
    public Vector2 GetScreenToWorld2D(Vector2 position)
    {
        return Raylib.GetScreenToWorld2D(position, _camera2D);
    }
    
    public Vector2 GetWorldToScreen2D(Vector2 position)
    {
        return Raylib.GetWorldToScreen2D(position, _camera2D);
    }
    
    internal Camera2D GetCamera2D()
    {
        return _camera2D;
    }
    
    public void BeginMode2D()
    {
        Raylib.BeginMode2D(_camera2D);
    }
    
    public void EndMode2D()
    {
        Raylib.EndMode2D();
    }

    public enum CameraFollowMode
    {
        Custom,
        Normal,
        Smooth
    }
}