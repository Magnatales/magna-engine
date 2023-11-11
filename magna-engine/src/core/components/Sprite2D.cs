using System.Numerics;
using Components;
using Core;
using Actors;
using Log;
using Raylib_cs;

namespace Motion;

public class Sprite2D : Component
{
    private readonly Actor _owner;
    private Shader? _shader;
    
    //Animation
    public bool FlipX { get; set; }
    private Texture2D Texture { get; }
    private Animation _currentAnimation;
    private readonly Dictionary<string, Animation> _allAnimations;
    private Rectangle _sourceRect;
    private Rectangle _destRec;

    //Frame
    private readonly FrameInfo _frameInfo;
    private int _currentFrame;

    public Sprite2D(Actor owner, Texture2D texture, Dictionary<string, Animation> animations, FrameInfo frameInfo)
    {
        _owner = owner;
        Texture = texture;
        _frameInfo = frameInfo;
        _allAnimations = animations;
        
        _currentAnimation = _allAnimations.First().Value;
        _currentFrame = _currentAnimation.StartingFrame;

        _sourceRect = new Rectangle(0.0f, 0.0f, (float)Texture.Width / _frameInfo.TotalFrames, Texture.Height);
        _sourceRect.X = _currentFrame * (float)Texture.Width / _frameInfo.TotalFrames;
        _destRec = new Rectangle(0.0f, 0.0f, _sourceRect.Width, _sourceRect.Height);
    }
    
    public void SetMaterial(Material material)
    {
        _shader = material.Shader;
    }

    public Shader? GetShader() => _shader;
    private float _timer;

    public override void Update()
    {
        //var spriteIndex = (int)(_frameTimePassed) % _currentAnimation.Length();

        _timer -= Time.Delta * _frameInfo.FramesSpeed;
        if (_timer <= 0)
        {
            _timer = 1f;
            _currentFrame++;
            if(_currentFrame >= _currentAnimation.Length())
                _currentFrame = _currentAnimation.StartingFrame;
            _sourceRect.X = _currentFrame * (float)Texture.Width / _frameInfo.TotalFrames;
        }
        // var spriteIndex = RaymathF.FloorToInt(Time.FrameCount / _frameInfo.FramesSpeed) % _currentAnimation.Length();
        // //Logger.ScreenLog(spriteIndex.ToString());
        // if (spriteIndex != _currentFrame)
        // {
        //     _currentFrame = spriteIndex;
        //     _sourceRect.Width = FlipX ? -Math.Abs(_sourceRect.Width) : Math.Abs(_sourceRect.Width);
        //     _sourceRect.X = _currentFrame * (float)Texture.Width / _frameInfo.TotalFrames;
        // }

        _sourceRect.Width = FlipX ? -Math.Abs(_sourceRect.Width) : Math.Abs(_sourceRect.Width);
        //_frameTimePassed += Time.Delta * 100;
        UpdateDestRect();
    }
    
    public void PlayAnimation(string animation)
    {
        _currentAnimation = _allAnimations[animation];
    }

    public override void Draw()
    {
        var destRectOrigin = new Vector2(MathF.Abs(_destRec.Width) / 2f, MathF.Abs(_destRec.Height) / 2f);
        Raylib.DrawTexturePro(Texture, _sourceRect, _destRec, destRectOrigin, _owner.Rotation, Color.WHITE);
    }
    public override void DrawDebug()
    {
        var destRectOrigin = new Vector2(MathF.Abs(_destRec.Width) / 2f, MathF.Abs(_destRec.Height) / 2f);
        Raylib.DrawCircle((int)_destRec.X, (int)_destRec.Y + 40, 8, Color.GREEN);
        Raylib.DrawRectanglePro(_destRec, destRectOrigin, _owner.Rotation, new Color(255, 0, 0, 125));
    }
    
    private void UpdateDestRect()
    {
        _destRec.X = _owner.Position.X;
        _destRec.Y = _owner.Position.Y;
        _destRec.Width = MathF.Abs(_sourceRect.Width) * _owner.Scale.X;
        _destRec.Height = MathF.Abs(_sourceRect.Height) * _owner.Scale.Y;
    }
}