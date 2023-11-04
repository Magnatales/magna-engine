namespace Motion;
using Entities;

public static class Sprites
{
    public static Sprite2D Scarfy(Actor owner)
    {
        var allAnimations = new Dictionary<string, Animation>
        {
            { Entities.Scarfy.ScarfyAnimations.Idle.ToString(), new Animation (2, 2) },
            { Entities.Scarfy.ScarfyAnimations.Run.ToString(), new Animation (0, 6) }
        };
        var frameInfo = new FrameInfo() { TotalFrames = 6, FramesSpeed = 6f };
        return new Sprite2D(owner,Textures.GetScarfy(), allAnimations, frameInfo);
    }

    public static Sprite2D ScarfyAuto(Actor owner)
    {
        var allAnimations = new Dictionary<string, Animation>
        {
            { Entities.Scarfy.ScarfyAnimations.Run.ToString(), new Animation (0, 6) },
            { Entities.Scarfy.ScarfyAnimations.Idle.ToString(), new Animation (2, 2) }
           
        };
        var frameInfo = new FrameInfo() { TotalFrames = 6, FramesSpeed = 6f };
        return new Sprite2D(owner, Textures.GetScarfy(), allAnimations, frameInfo);
    }
}