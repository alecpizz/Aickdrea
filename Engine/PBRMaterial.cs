namespace Engine;
using Raylib_cs.BleedingEdge;
using rlImGui_cs;

using static Raylib_cs.BleedingEdge.Raylib;

public class PBRMaterial
{
    public struct TextureProperty
    {
        public Color Color;
        public bool UseTexture = false;
        public Texture2D Texture;

        public TextureProperty()
        {
            Color = Color.White;
            UseTexture = false;
        }
    }

    private TextureProperty _albedo;
    private TextureProperty _normal;

    public PBRMaterial()
    {
        _albedo = new TextureProperty();
        
        _normal = new TextureProperty();
        _normal.Color = new Color(0.5F, 0.5F, 1.0F, 1.0F);
    }

    public void SetAlbedo(Color color)
    {
        _albedo.Color = color;
    }
    
    public void SetAlbedo(Color color, Texture2D tex)
    {
        _albedo.UseTexture = true;
        _albedo.Texture = tex;
        
        SetAlbedo(color);
    }

    public void SetNormal(Texture2D tex)
    {
        _normal.UseTexture = true;
        _normal.Texture = tex;
    }
}