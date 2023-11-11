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

    private static Texture2D _particle;

    public static Texture2D GetParticle()
    {
        if (!Raylib.IsTextureReady(_particle))
            _particle = Raylib.LoadTexture("resources/circle-filled.png");
        
        return _particle;
    }
    
    private static Texture2D _oneByOne;
    public static Texture2D GetOneByOne()
    {
        if (!Raylib.IsTextureReady(_oneByOne))
            _oneByOne = Raylib.LoadTexture("resources/1x1.png");
        
        return _oneByOne;
    }
    
    private static Texture2D _snowflake;
    public static Texture2D GetSnowFlake()
    {
        if (!Raylib.IsTextureReady(_snowflake))
            _snowflake = Raylib.LoadTexture("resources/snowflake.png");
        
        return _snowflake;
    }
}