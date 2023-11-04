using Raylib_cs;

public static class Textures
{
    private static Texture2D _scarfyTexture;
    public static Texture2D GetScarfy()
    {
        if (!Raylib.IsTextureReady(_scarfyTexture))
            _scarfyTexture = Raylib.LoadTexture("resources/scarfy.png");
        return _scarfyTexture;
    }
}