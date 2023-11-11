using System.Numerics;
using Actors;
using Core;
using Core.modules.particles_system;
using Helpers;
using Helpers.quadtree;
using Log;
using Motion;
using Raylib_cs;

namespace HelloWorld;

public class TestYSortingScene : Scene
{
    private Scarfy _scarfy;
    private Quadtree _quadtree;
    public TestYSortingScene(string name) : base(name)
    {
        
    }
    
    private float rectangleSizeX = 400; // Adjust the size as needed
    private float rectangleSizeY = 400;
    private float sizeSpeed = 1000;
    public override void Init()
    {
        base.Init();
        var random = new Random();
        _quadtree = new Quadtree(new Rectangle(0, 0, 1000, 1000), 12);
        var actors = new List<Actor>();
        // for (var i = 0; i < 10000; i++)
        // {
        //     var randomX = random.Next(-4400, Raylib.GetScreenWidth() + 4400);
        //     var randomY = random.Next(-4800, Raylib.GetScreenHeight() + 4800);
        //
        //     var emptyActor = new EmptyActor(new Vector2(randomX, randomY));
        //     emptyActor.AddComponent(Sprites.ScarfyAuto(emptyActor));
        //     actors.Add(emptyActor);
        // }
        
        _scarfy = new Scarfy(new Vector2(300, 200));
        Cam2D.SetTarget(_scarfy);
        actors.Add(_scarfy);
        
        // var ps = new ParticleEmitter(new IStaticEmitter(new Vector2(100, 200)), ParticlesFactory.Fire());
        // actors.Add(ps);
        //
        // var ps1 = new ParticleEmitter(new IStaticEmitter(new Vector2(500, 200)), ParticlesFactory.MagicSparkle());
        // actors.Add(ps1);
 
        var ps2 = new ParticleEmitter(new ILineEmitter(new Vector2(0, 10), new Vector2(800, 10)), ParticlesFactory.Snow());
        actors.Add(ps2);
        
        // var ps3 = new ParticleEmitter(new IStaticEmitter(new Vector2(300, 100)), ParticlesFactory.GreenExplosion());
        // actors.Add(ps3);
        //
        var ps4 = new ParticleEmitter(new IStaticEmitter(new Vector2(280, 200)), ParticlesFactory.BloodHit());
        actors.Add(ps4);
        //
        // var particlesMouse = new ParticleEmitter(new IWorldMouseEmitter(Cam2D), ParticlesFactory.Basic());
        // actors.Add(particlesMouse);
        
        _quadtree.AddActors(actors);
        _quadtree.OnQuadTreeUpdate += UpdateActors;
    }
    

    private void UpdateActors(List<Actor> obj)
    {
        obj.ForEach(x =>
        {
            x.Update(Time.Delta);
        });
    }

    protected internal override void Update(float dt)
    {
        if (Input.IsKeyDown(KeyboardKey.KEY_Q))
        {
            rectangleSizeX += sizeSpeed * dt;
            rectangleSizeY += sizeSpeed * dt;
        }
        else if (Input.IsKeyDown(KeyboardKey.KEY_E))
        {
            rectangleSizeX -= sizeSpeed * dt;
            rectangleSizeY -= sizeSpeed * dt;
        }
        // Get the current mouse position in screen space
        Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
    
        // Convert the mouse position from screen space to world space using the camera
        Vector2 worldMousePosition = Raylib.GetScreenToWorld2D(mousePosition, Cam2D.GetCamera2D());

        // if (Input.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
        // {
        //     var ps = new SparkParticles(new IStaticEmitter(worldMousePosition));
        //     var actors = new List<Actor> { ps };
        //     _quadtree.AddActors(actors);
        // }
        //
        // if (Input.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        // {
        //     var ps = new SparkParticles(new IStaticEmitter(worldMousePosition));
        //     var actors = new List<Actor> { ps };
        //     _quadtree.AddActors(actors);
        // }
    
        // Calculate the position for the rectangle to be at the center of the mouse
        float halfSizeX = rectangleSizeX / 2;
        float halfSizeY = rectangleSizeY / 2;
    
        Rectangle rectangle = new Rectangle(
            worldMousePosition.X - halfSizeX,
            worldMousePosition.Y - halfSizeY,
            rectangleSizeX,
            rectangleSizeY
        );
        
        Camera2D camera = Cam2D.GetCamera2D();

// Calculate the world coordinates of the top-left and bottom-right corners of the screen space viewport
        Vector2 topLeft = Raylib.GetScreenToWorld2D(new Vector2(0, 0), camera);
        Vector2 bottomRight = Raylib.GetScreenToWorld2D(new Vector2(Window.GetScreenWidth(), Window.GetScreenHeight()), camera);
        
        float offsetX = 100.0f; // Replace with your desired X offset
        float offsetY = 50.0f;  // Replace with your desired Y offset
        topLeft.X -= offsetX;
        topLeft.Y -= offsetY;
        bottomRight.X += offsetX;
        bottomRight.Y += offsetY;

// Create a rectangle in world space using the converted coordinates
        Rectangle cameraViewport = new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        _quadtree.Update(cameraViewport, dt);
        Cam2D.Update();
    }

    protected internal override void Draw()
    {
        Cam2D.BeginMode2D();
        _quadtree.Draw();
        //_quadtree.DrawDebug();
        Raylib.DrawRectangleLines(-400, 231, 1500, 400, Color.BLACK);
        Cam2D.EndMode2D();
        Raylib.DrawTextEx(Fonts.DefaultBold,"Total particles: " + ParticleEmitter.Count.ToString(), new Vector2(10, 10), Fonts.DefaultBold.BaseSize, 2, Color.WHITE);
        MouseHelper.DrawMouseWorldPosition(Cam2D.GetCamera2D());
    }
}