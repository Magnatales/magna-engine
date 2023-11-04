using System.Numerics;
using Core;
using Core.Tweening;
using Motion;
using Raylib_cs;

namespace Entities;

public class Scarfy : Actor
{
    public override string Id => "Scarfy";
    private const float SPEED = 300f;
    private float _speedMultiplier = 1;
    private Tweener _tweener;

    public enum ScarfyAnimations { Idle, Run, }
    private readonly Sprite2D _sprite2D;

    public Scarfy(Vector2 worldPosition) : base(worldPosition)
    {
        _sprite2D = AddComponent(Sprites.Scarfy(this));
        _tweener = new Tweener();
    }

    public void Grow()
    {
        _tweener.TweenTo(this, size => Scale, new Vector2(2, 2), 0.5f).Easing(Ease.Sinusoidal.InOut);
    }
    
    public void Normal()
    {
        _tweener.TweenTo(this, size => Scale, new Vector2(1, 1), 0.5f).Easing(Ease.Sinusoidal.InOut);
    }
    
    public void Small()
    {
        _tweener.TweenTo(this, size => Scale, new Vector2(0.5f, 0.5f), 0.5f).Easing(Ease.Sinusoidal.InOut);
    }
    

    public override void Update(float dt)
    {
        base.Update(dt);
        _tweener.Update(dt);
        var movement = Vector2.Zero;
        
        if(Input.IsKeyPressed(KeyboardKey.KEY_ONE))
            Grow();
        
        if(Input.IsKeyPressed(KeyboardKey.KEY_TWO))
            Normal();
        
        if(Input.IsKeyPressed(KeyboardKey.KEY_THREE))
            Small();

        if (Input.IsKeyDown(KeyboardKey.KEY_A))
        {
            _sprite2D.FlipX = true;
            movement.X -= SPEED;
        }
        if (Input.IsKeyDown(KeyboardKey.KEY_D))
        {
            _sprite2D.FlipX = false;
            movement.X += SPEED;
        }
        if (Input.IsKeyDown(KeyboardKey.KEY_W))
            movement.Y -= SPEED;
        if(Input.IsKeyDown(KeyboardKey.KEY_S))
            movement.Y += SPEED;

        var shiftPressed = Input.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);
        _speedMultiplier = shiftPressed ? 1.5f : 1f;

        if (movement != Vector2.Zero)
        {
            _sprite2D.PlayAnimation(ScarfyAnimations.Run.ToString());
            movement = Vector2.Normalize(movement);
        }
        else
        {
            _sprite2D.PlayAnimation(ScarfyAnimations.Idle.ToString());
        }
        
        var rotationSpeed = 180f;
        if (Input.IsKeyDown(KeyboardKey.KEY_R))
        {
            Rotation += rotationSpeed * dt;
            Rotation  %= 360;
        }

        Position += movement * SPEED * _speedMultiplier * dt;
    }
}