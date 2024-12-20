namespace Engine;
using Raylib_cs.BleedingEdge;
using rlImGui_cs;
using static Raylib_cs.BleedingEdge.Raylib;

public unsafe class PBRShader
{
    public static Material InitPBRMaterial(
        Texture2D albedo,
        Texture2D normal,
        Texture2D roughness,
        TextureFilter filterMode)
    {
        // Init shader
        Shader shader = LoadShader(
            "Resources/Shaders/pbr.vert",
            "Resources/Shaders/pbr.frag"
        );

        shader.Locs[(int)ShaderLocationIndex.MapAlbedo] = GetShaderLocation(
            shader,
            "albedoMap"
        );
        shader.Locs[(int)ShaderLocationIndex.MapNormal] = GetShaderLocation(
            shader,
            "normalMap"
        );
        shader.Locs[(int)ShaderLocationIndex.MapRoughness] = GetShaderLocation(
            shader,
            "roughnessMap"
        );
        
        // Init material
        Material mat = LoadMaterialDefault();
        mat.Shader = shader;

        mat.Maps[(int)MaterialMapIndex.Albedo].Texture = albedo;
        mat.Maps[(int)MaterialMapIndex.Normal].Texture = normal;
        mat.Maps[(int)MaterialMapIndex.Roughness].Texture = roughness;
        
        SetTextureFilter(mat.Maps[(int)MaterialMapIndex.Albedo].Texture, filterMode);
        SetTextureFilter(mat.Maps[(int)MaterialMapIndex.Normal].Texture, filterMode);
        SetTextureFilter(mat.Maps[(int)MaterialMapIndex.Roughness].Texture, filterMode);
        
        GenTextureMipmaps(mat.Maps[(int)MaterialMapIndex.Albedo].Texture);
        GenTextureMipmaps(mat.Maps[(int)MaterialMapIndex.Normal].Texture);
        GenTextureMipmaps(mat.Maps[(int)MaterialMapIndex.Roughness].Texture);

        return mat;
    }
}