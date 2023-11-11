using System.Numerics;
using Actors;
using Components;
using Core;
using Helpers;
using Helpers.quadtree;
using Raylib_cs;

namespace HelloWorld;

public class TestQuadtreeScene : Scene
{
    public TestQuadtreeScene(string name, bool useQuadtree) : base(name)
    {
        _useQuadtree = useQuadtree;
    }

    private Quadtree _quadtree;
    private bool _useQuadtree;
    private readonly List<Actor> _currentActors = new();
    private Rectangle view;
    private Scarfy _scarfy;
    private PhysicsSimulation _physicsSimulation;

    public override void Init()
    {
        base.Init();
        var numberOfActorsToSpawn = 2000;
        var areaWidth = 800;
        var areaHeight = 800;
        var screenX = Raylib.GetScreenWidth() / 2f;
        var screenY = Raylib.GetScreenHeight() / 2f;
        view = RectangleUtils.CreateCentered(new Vector2(screenX, screenY), 800, 800);
        _quadtree = new Quadtree( RectangleUtils.CreateCentered(new Vector2(screenX, screenY), 1000, 1000),4);
        for (var i = 0; i < numberOfActorsToSpawn; i++)
        {
            var randomOffset = new Vector2(Raylib.GetRandomValue(-areaWidth / 2, areaWidth / 2),
                Raylib.GetRandomValue(-areaHeight / 2, areaHeight / 2));

            var actor = new EmptyActor(new Vector2(screenX, screenY) + randomOffset);
            actor.AddComponent(new CirclePhysics(actor, 2, _quadtree, _currentActors, _useQuadtree));
            //AddActorAndOverrideId(actor, actor.Id + i);
            _currentActors.Add(actor);
        }
        _quadtree.AddActors(_currentActors);
        _scarfy = new Scarfy(new Vector2(screenX, screenY));
        Window.Maximize();
        _physicsSimulation = new PhysicsSimulation(_quadtree);
    }

    protected internal override void Draw()
    {
        Cam2D.BeginMode2D();
        //base.Draw();
        Raylib.DrawRectangleLinesEx(view, 4, Color.PURPLE);
        _quadtree.Draw();
        _quadtree.DrawDebug();
        Cam2D.EndMode2D();
        //DrawWidgets();
        var screenX = Raylib.GetScreenWidth() / 2f;
        if(_useQuadtree)
            Raylib.DrawTextEx(Fonts.Default, $"Quadtree capacity {_quadtree.Capacity}\n{_quadtree}", new Vector2(screenX -400, 5), Fonts.Default.BaseSize, 2, Color.WHITE);
        else
            Raylib.DrawTextEx(Fonts.Default, $"{_currentActors.Count} Actors checking for collisions", new Vector2(500, 20), Fonts.Default.BaseSize, 2, Color.WHITE);
        
        Raylib.DrawTextEx(Fonts.Default, $"Viewport X {rectangleSizeX:F0}\nViewport Y {rectangleSizeY:F0}", new Vector2(20, 5), Fonts.Default.BaseSize, 2, Color.WHITE);
    }


    private float rectangleSizeX = 400; // Adjust the size as needed
    private float rectangleSizeY = 300;
    private float sizeSpeed = 200f;
    protected internal override void Update(float dt)
    {
        Cam2D?.Update();
        _scarfy?.Update(dt);
        Cam2D?.SetTarget(_scarfy);

        if (Input.IsKeyPressed(KeyboardKey.KEY_F11))
        {
            Window.ToggleFullscreen();
        }

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

// Create a rectangle in world space using the converted coordinates
        Rectangle cameraViewport = new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
    
        // Update the Quadtree with the new rectangle
        _quadtree.Update(cameraViewport, dt);
    }

    public override void Dispose()
    {
        base.Dispose();
        _physicsSimulation?.Dispose();
    }
}