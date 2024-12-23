using System.Numerics;
using Raylib_cs.BleedingEdge;
using static Raylib_cs.BleedingEdge.Raylib;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace Engine.Entities;

public unsafe class SkyboxEntityPBR : Entity
{
    private Shader _cubeShader;
    private Texture2D _cubeMap;
    private Model _cube;
    
    private const string CubePathVert = @"Resources\Shaders\PBRIncludes\cubemap.vert";
    private const string CubePathFrag = @"Resources\Shaders\PBRIncludes\cubemap.frag";

    public SkyboxEntityPBR(string cubeMap) : base(cubeMap)
    {
        SetupCaptureCube();
        CaptureCubemap();
    }

    private void SetupCaptureCube()
    {
        // Load shader
        _cubeShader = LoadShader(CubePathVert, CubePathFrag);

        // Setup uniform locations
        _cubeShader.Locs[(int)ShaderLocationIndex.MapAlbedo] = GetShaderLocation(
            _cubeShader,
            "equirectangularMap"
        );
        
        // Gen cubemap capture mesh
        Mesh cube = GenMeshCube(1.0F, 1.0F, 1.0F);
        _cube = LoadModelFromMesh(cube);
        
        // Create sky material
        Material mat = LoadMaterialDefault();
        mat.Shader = _cubeShader;
        
        // Load texture
        _cubeMap = LoadTexture("Resources/Textures/petit_port_2k.png");
        
        // Set texture in material
        mat.Maps[(int)MaterialMapIndex.Albedo].Texture = _cubeMap;
        
        // Set material on model
        for (int i = 0; i < _cube.MaterialCount; i++)
        {
            _cube.Materials[i] = mat;
        }
    }

    private void CaptureCubemap()
    {
        // Create custom render/framebuffers
        int captureFbo, captureRbo;
        GL.GenFramebuffers(1, &captureFbo);
        GL.GenRenderbuffers(1, &captureRbo);
        
        // Store 6 cubemap sides
        RenderTexture2D[] cubemapRts = new RenderTexture2D[6];

        for (int i = 0; i < cubemapRts.Length; i++)
        {
            cubemapRts[i] = LoadRenderTexture(512, 512);
        }

        // Projection details for the capture
        Matrix4x4 captureProjection = Matrix4x4.CreatePerspectiveFieldOfView(
            90.0F * Deg2Rad,
            1.0F,
            0.1F,
            1000.0F
        );
        
        // Views to capture (= sides of the cubemap)
        Matrix4x4[] captureViews =
        [
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(1.0F, 0.0F, 0.0F),
                new Vector3(0.0F, -1.0F, 0.0F)),
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(-1.0F, 0.0F, 0.0F),
                new Vector3(0.0F, -1.0F, 0.0F)),
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(0.0F, 1.0F, 0.0F),
                new Vector3(0.0F, 0.0F, 1.0F)),
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(0.0F, -1.0F, 0.0F),
                new Vector3(0.0F, 0.0F, -1.0F)),
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(0.0F, 0.0F, 1.0F),
                new Vector3(0.0F, -1.0F, 0.0F)),
            Matrix4x4.CreateLookAt(
                new Vector3(0.0F, 0.0F, 0.0F),
                new Vector3(0.0F, 0.0F, -1.0F),
                new Vector3(0.0F, -1.0F, 0.0F))
        ];
        
        // Send view projection to shader
        SetCubeProjectionMatrix(captureProjection);
        
        // Set viewport to proper dimensions (temporarily)
        GL.Viewport(0, 0, 512, 512);
        
        // Render cubemap sides
        for (int i = 0; i < captureViews.Length; i++)
        {
            
        }
    }

    private void SetCubeProjectionMatrix(Matrix4x4 value)
    {
        SetShaderValueMatrix(
            _cubeShader,
            GetShaderLocation(
                _cubeShader,
                "matProjection"),
            value
        );
    }
    
    private void SetCubeViewMatrix(Matrix4x4 value)
    {
        SetShaderValueMatrix(
            _cubeShader,
            GetShaderLocation(
                _cubeShader,
                "matView"),
            value
        );
    }

    public override void OnRender()
    {
        Rlgl.DisableBackfaceCulling();
        Rlgl.DisableDepthMask();
        DrawModel(_cube, Vector3.Zero, 100.0F, Color.White);
        Rlgl.EnableBackfaceCulling();
        Rlgl.EnableDepthMask();
    }

    public override void OnCleanup()
    {
        UnloadTexture(_cubeMap);
        UnloadShader(_cubeShader);
        UnloadModel(_cube);
    }
}