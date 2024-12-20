using System.Numerics;
using ImGuiNET;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs.BleedingEdge;
using rlImGui_cs;
using static Raylib_cs.BleedingEdge.Raylib;

namespace Engine;

public unsafe class Engine
{
    private bool _exitWindow;
    private float _currentTime;
    private float _accumulator;
    private PhysDrawer _physDrawer;
    private Player _player;
    private World _world;
    private Sound _sound;
    private Shader _pbrShader;
    private List<PBRModel> _cityMeshes;
    private Shader _skyShader;
    private Model _skyBox;
    private Image _skyTexture;
    private Texture2D _cubeMap;
    private float _t;
    private Camera3D _camera;
    private bool _uiActive;
    private RigidBody _cityBody;
    private PlayerRayCaster _playerRayCaster;
    private bool _firstMouse = false;

    public Engine()
    {
        const int screenWidth = 1280;
        const int screenHeight = 720;

        SetConfigFlags(ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.WindowResizable);
        InitWindow(screenWidth, screenHeight, "My Window!");
        InitAudioDevice();
        int fps = GetMonitorRefreshRate(GetCurrentMonitor());
        SetTargetFPS(fps);

        _sound = LoadSound(@"Resources\Sounds\tada.mp3");

        _camera = new Camera3D()
        {
            Position = new Vector3(2.0f, 4.0f, 6.0f),
            Target = new Vector3(0.0f, 0.5f, 0.0f),
            Up = new Vector3(0.0f, 1.0f, 0.0f),
            FovY = 45.0f,
            Projection = CameraProjection.Perspective
        };

        _world = new World();
        _world.SubstepCount = 4;


        for (int i = 0; i < 20; i++)
        {
            RigidBody body = _world.CreateRigidBody();
            body.AddShape(new BoxShape(1));
            body.Position = new JVector(0, i * 2 + 0.5f, 0);
        }

        SetExitKey(KeyboardKey.Null);
        rlImGui.Setup();
        ImGUIUtils.SetupSteamTheme();
        

        float t = 0.0f;
        float dt = 1.0f / fps;

        _currentTime = (float)GetTime();

        _pbrShader = LoadShader(
            @"Resources\Shaders\pbr.vert",
            @"Resources\Shaders\pbr.frag"
        );

        Model cone = LoadModel(@"Resources\Models\ConeTest\ConeTestModel.glb");

        Model city = LoadModel(@"Resources\Models\GM Big City\scene.gltf");
        _cityMeshes = new List<PBRModel>();
        
        for (int i = 0; i < city.MeshCount; i++)
        {
            int matIndex = city.MeshMaterial[i];
            Material mat = city.Materials[matIndex];
            
            Model chunk = LoadModelFromMesh(city.Meshes[i]);

            PBRModel pbrModel = new PBRModel(chunk);
            
            /*pbrModel.SetAlbedo(
                city.Materials[matIndex].Maps[(int)MaterialMapIndex.Albedo].Texture,
                true,
                TextureFilter.Bilinear
            );
            pbrModel.SetNormal(
                city.Materials[matIndex].Maps[(int)MaterialMapIndex.Normal].Texture
            );*/
            
            _cityMeshes.Add(pbrModel);
        }
        
        for (int i = 0; i < cone.MeshCount; i++)
        {
            int matIndex = cone.MeshMaterial[i];
            Material mat = cone.Materials[matIndex];
            
            Model chunk = LoadModelFromMesh(cone.Meshes[i]);

            PBRModel pbrModel = new PBRModel(cone);
            pbrModel.SetNormal(LoadTexture(
                @"Resources\Models\ConeTest\tmjjddkhw_2K_Normal_LOD1.jpg"
            ));
            
            _cityMeshes.Add(pbrModel);
        }
       

        _cityBody = _world.CreateRigidBody();
        List<JTriangle> tris = new List<JTriangle>();

        for (int i = 0; i < city.MeshCount; i++)
        {
            var mesh = city.Meshes[i];
            Vector3* vertdata = (Vector3*)mesh.Vertices;
            if (mesh.Indices != null)
            {
                for (int j = 0; j < mesh.TriangleCount; j++)
                {
                    JVector a = vertdata[mesh.Indices[j * 3 + 0]].ToJVector();
                    JVector b = vertdata[mesh.Indices[j * 3 + 1]].ToJVector();
                    JVector c = vertdata[mesh.Indices[j * 3 + 2]].ToJVector();
                    JVector normal = (c - b) % (a - b);

                    if (MathHelper.CloseToZero(normal, 1e-12f))
                    {
                        continue;
                    }

                    tris.Add(new JTriangle(b, a, c));
                }
            }
        }

        var jtm = new TriangleMesh(tris);
        List<RigidBodyShape> triangleShapes = new List<RigidBodyShape>();
        for (int i = 0; i < jtm.Indices.Length; i++)
        {
            TriangleShape ts = new TriangleShape(jtm, i);
            triangleShapes.Add(ts);
        }


        _cityBody.AddShape(triangleShapes, false);
        _cityBody.Position = city.Transform.Translation.ToJVector();

        _cityBody.IsStatic = true;
        _physDrawer = new PhysDrawer();
        _player = new Player(_world, _camera.Position.ToJVector());
        _playerRayCaster = new PlayerRayCaster(_world);
        _skyShader = LoadShader(@"Resources\Shaders\skybox.vert", @"Resources\Shaders\skybox.frag");
        Mesh cube = GenMeshCube(1.0f, 1.0f, 1.0f);
        _skyBox = LoadModelFromMesh(cube);
        SetShaderValue(_skyShader, GetShaderLocation(_skyShader, "environmentMap"),
            (int)MaterialMapIndex.Cubemap, ShaderUniformDataType.Int);
        SetMaterialShader(ref _skyBox, 0, ref _skyShader);
        _skyTexture = LoadImage(@"Resources\Textures\cubemap.png");
        _cubeMap = LoadTextureCubemap(_skyTexture, CubemapLayout.AutoDetect);
        SetMaterialTexture(_skyBox.Materials, MaterialMapIndex.Cubemap, _cubeMap);
        Time.FixedDeltaTime = dt;
    }

    public void Run()
    {
        while (!WindowShouldClose() && !_exitWindow)
        {
            //delta time
            float newTime = (float)GetTime();
            float frameTime = newTime - _currentTime;
            if (frameTime > 0.25)
                frameTime = 0.25f;
            Time.DeltaTime = frameTime;
            _currentTime = newTime;

            _accumulator += frameTime;
            //physics updates
            while (_accumulator >= Time.FixedDeltaTime)
            {
                _t += Time.FixedDeltaTime;
                _world.Step(Time.FixedDeltaTime, true);
                _accumulator -= Time.FixedDeltaTime;
            }

            //player
            _player?.Update(ref _camera);
            _playerRayCaster.Update(_camera);

            if (ImGui.GetIO().WantCaptureMouse || _uiActive)
            {
            }
            else
            {
                // UpdateCamera(ref camera, CameraMode.Free);
                if (!_firstMouse && IsMouseButtonPressed(MouseButton.Left))
                {
                    DisableCursor();
                    _firstMouse = true;
                }
            }

            if (!ImGui.GetIO().WantCaptureKeyboard)
            {
                if (IsKeyPressed(KeyboardKey.E))
                {
                    RigidBody body = _world.CreateRigidBody();
                    body.AddShape(new BoxShape(1));
                    body.Position = _camera.Position.ToJVector();
                }

                if (IsKeyPressed(KeyboardKey.Escape))
                {
                    _uiActive = !_uiActive;
                    if (!_uiActive)
                    {
                        DisableCursor();
                    }
                    else
                    {
                        EnableCursor();
                    }
                }
            }


            BeginDrawing();

            ClearBackground(Color.RayWhite);

            BeginMode3D(_camera);

            //funny skybox
            Rlgl.DisableBackfaceCulling();
            Rlgl.DisableDepthMask();
            DrawModel(_skyBox, Vector3.Zero, 1.0f, Color.White);
            Rlgl.EnableBackfaceCulling();
            Rlgl.EnableDepthMask();


            foreach (var body in _world.RigidBodies)
            {
                if (body == _world.NullBody || body == _cityBody || body == _player?.Body)
                    continue; // do not draw this
                body.DebugDraw(_physDrawer);
            }

            foreach (PBRModel model in _cityMeshes)
            {
                model.DrawModel(ref _pbrShader);
            }

            DrawGrid(10, 1.0f);

            foreach (var pt in _playerRayCaster._hitPoints)
            {
                DrawSphere(pt, 0.2f, Color.Red);
            }

            EndMode3D();

            rlImGui.Begin();
            if (_uiActive && ImGui.Begin("who needs an engine Engine", ref _uiActive, ImGuiWindowFlags.MenuBar))
            {
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("Goober"))
                    {
                        if (ImGui.MenuItem("Close"))
                        {
                            _uiActive = false;
                        }

                        if (ImGui.MenuItem("Quit Program"))
                        {
                            _exitWindow = true;
                        }

                        ImGui.EndMenu();
                    }

                    ImGui.EndMenuBar();
                }

                if (ImGui.Button("Play Sound"))
                {
                    PlaySound(_sound);
                }

                if (ImGui.Button("Spawn Cube"))
                {
                    RigidBody body = _world.CreateRigidBody();
                    body.AddShape(new BoxShape(1));
                    body.Position = new JVector(0, 10, 0);
                }

                if (ImGui.Button("Respawn Player"))
                {
                    if (_player != null)
                    {
                        _world.Remove(_player.Body);
                    }

                    _camera.Position = new Vector3(2.0f, 4.0f, 6.0f);
                    _player = new Player(_world, new JVector(2.0f, 4.0f, 6.0f));
                }
            }

            DrawFPS(10, 10);

            DrawText($"Player Velocity {_player.Body.Velocity.Length()}", 10, 20, 20, Color.White);

            ImGui.End();
            rlImGui.End();

            EndDrawing();
        }
    }

    public void Cleanup()
    {
        UnloadShader(_skyShader);
        UnloadImage(_skyTexture);
        UnloadTexture(_cubeMap);
        UnloadSound(_sound);
        CloseAudioDevice();
        CloseWindow();
    }
}