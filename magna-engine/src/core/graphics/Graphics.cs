// Decompiled with JetBrains decompiler
// Type: Sparkle.csharp.graphics.Graphics
// Assembly: Sparkle, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 90591F6E-6AA8-49C7-85B5-2B18ABACA428
// Assembly location: C:\Users\l791\.nuget\packages\sparkle\2.0.1\lib\net7.0\Sparkle.dll
// XML documentation location: C:\Users\l791\.nuget\packages\sparkle\2.0.1\lib\net7.0\Sparkle.xml

using Raylib_cs;

namespace Core;
public static class Graphics
  {
    public static void ClearBackground(Color color) => Raylib.ClearBackground(color);
    
    public static void BeginDrawing() => Raylib.BeginDrawing();
    
    public static void EndDrawing() => Raylib.EndDrawing();
    
    public static void BeginTextureMode(RenderTexture2D target) => Raylib.BeginTextureMode(target);
    
    public static void EndTextureMode() => Raylib.EndTextureMode();
    
    public static void BeginShaderMode(Shader shader) => Raylib.BeginShaderMode(shader);
    
    public static void EndShaderMode() => Raylib.EndShaderMode();
    
    public static void BeginBlendMode(BlendMode mode) => Raylib.BeginBlendMode(mode);
    
    public static void EndBlendMode() => Raylib.EndBlendMode();
    
    public static void BeginScissorMode(int x, int y, int width, int height) => Raylib.BeginScissorMode(x, y, width, height);
    
    public static void EndScissorMode() => Raylib.EndScissorMode();
  }
