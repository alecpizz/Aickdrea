using System.Numerics;

namespace Engine;
using Raylib_cs.BleedingEdge;
using rlImGui_cs;

using static Raylib_cs.BleedingEdge.Raylib;

public unsafe class PBRModel
{
    public struct TextureSlot
    {
        public Texture2D Texture;
        public int Location;
    }

    private Material _modelMaterial;
    private Model _model;

    private TextureSlot _albedo;
    private TextureSlot _normal;

    public PBRModel(Model model, Shader shader)
    {
        _model = model;

        _modelMaterial = LoadMaterialDefault();
        _modelMaterial.Shader = shader;
        
        GetShaderLocations(shader);
        GrabTexturesFromModel();

        _model.Materials[0] = _modelMaterial;
    }

    private void GetShaderLocations(Shader shader)
    {
        _albedo.Location = GetShaderLocation(shader, "albedoMap");
        _normal.Location = GetShaderLocation(shader, "normalMap");
    }

    private unsafe void GrabTexturesFromModel()
    {
        if (_model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture.Id != 0)
        {
            _albedo.Texture = _model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture;
        }
        
        if (_model.Materials[0].Maps[(int)MaterialMapIndex.Normal].Texture.Id != 0)
        {
            _normal.Texture = _model.Materials[0].Maps[(int)MaterialMapIndex.Normal].Texture;
        }
    }

    public void DrawModel(ref Shader shader)
    {
        // Draw model
        Raylib.DrawModelEx(
            _model,
            Vector3.Zero,
            Vector3.UnitY,
            0.0F,
            Vector3.One,
            Color.Beige
        );
    }

    public void SetAlbedo(
        Texture2D texture, 
        bool genMipmaps, 
        TextureFilter filterMode)
    {
        if (genMipmaps)
        {
            texture.Mipmaps = 4;
            GenTextureMipmaps(texture);
        }
        
        SetTextureFilter(texture, filterMode);
        
        _modelMaterial.Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
    }
    
    public void SetNormal(Texture2D texture)
    {
        _normal.Texture = texture;
    }
}