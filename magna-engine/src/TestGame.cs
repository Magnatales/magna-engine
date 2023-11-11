using Core;
using Helpers;
using Raylib_cs;

public class TestGame : Core.Game
{
    public TestGame(GameSettings settings) : base(settings)
    {
    }

    protected override void Load()
    {
        base.Load();
    }

    protected override void Draw()
    {
        base.Draw();
        ScreenDebuggers.DrawFps(Fonts.Default, Color.BLACK);
    }
}