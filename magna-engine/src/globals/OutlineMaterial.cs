using Raylib_cs;

public class OutlineMaterial : Material
{
    public OutlineMaterial(Texture2D texture2D, float[] outlineColor, float outlineSize)
    {
        _shader = Raylib.LoadShader(null, "resources/shaders/outline.fs");
        
        SetOutlineColor(outlineColor);
        SetOutlineSize(outlineSize);
        SetTextureSize(texture2D);
    }
    
    private void SetOutlineColor(float[] color)
    {
        var outlineColorLoc = Raylib.GetShaderLocation(_shader, "outlineColor");
        Raylib.SetShaderValue(_shader, outlineColorLoc, color, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
    }

    private void SetOutlineSize(float size)
    {
        var outlineSizeLoc = Raylib.GetShaderLocation(_shader, "outlineSize");
        Raylib.SetShaderValue(_shader, outlineSizeLoc, size, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);
    }

    private void SetTextureSize(Texture2D texture2D)
    {
        float[] textureSize = { texture2D.Width, texture2D.Height };
        var textureSizeLoc = Raylib.GetShaderLocation(_shader, "textureSize");
        Raylib.SetShaderValue(_shader, textureSizeLoc, textureSize, ShaderUniformDataType.SHADER_UNIFORM_VEC2);
    }
}

public class Material
{
    protected Shader _shader;
    public Shader Shader => _shader;

}