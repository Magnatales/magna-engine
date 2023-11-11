using System.Numerics;
using Components;
using Core;
using Actors;
using Game.UI;
using Helpers;
using Log;
using Motion;
using Raylib_cs;
public class TestScene : Scene
{
    private Scarfy _scarfy;
    private Cell[,] _cells;
    private Cell[,] _cellShadows;
    private int x = 90;
    private int y = 90;
    public TestScene(string name) : base(name)
    {
        
    }

    public override void Init()
    {
        _scarfy = new Scarfy(new Vector2(350f, 280f));
        _scarfy.AddComponent(new PositionLogger(_scarfy, _scarfy.Id));
        _scarfy.AddComponent(new RotationLogger(_scarfy));
        AddActor(_scarfy);
        Cam2D.SetTarget(_scarfy);
        
        Random random = new Random();
        
        for (var i = 0; i < 1; i++)
        {
            var randomX = random.Next(-2400, Raylib.GetScreenWidth() + 2400);
            var randomY = random.Next(-2800, Raylib.GetScreenHeight() + 2800);
        
            var entitySprite = new EmptyActor(new Vector2(randomX, randomY));
            entitySprite.AddComponent(Sprites.ScarfyAuto(entitySprite));
            AddActorAndOverrideId(entitySprite, entitySprite.Id + " " + i);
        }
        
        _cells = new Cell[x,y];
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                _cells[i, j] = new Cell(new Vector2(i * 80,j * 80), new Vector2(74, 74), RaymathF.RandValue() ? Color.BLUE : Color.PURPLE);
            }
        }
        _cellShadows = new Cell[x,y];
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                _cellShadows[i, j] = new Cell(new Vector2(i * 80,j * 80), new Vector2(80, 80), Color.BLACK);
            }
        }
        
        AddWidget(new GamePanel());
    }

    protected internal override void Update(float dt)
    {
        base.Update(dt);

        if (Input.IsKeyPressed(KeyboardKey.KEY_P))
        {
            Logger.Debug("Hey!");
            Logger.ScreenLog("Heyyyyyyy");
            Logger.ScreenLog(new Vector2(10, 30).ToString());
        }
        
        if (Input.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
        {
            Window.Close();
        }

        if (Input.IsKeyPressed(KeyboardKey.KEY_F11) || (Input.IsKeyDown(KeyboardKey.KEY_LEFT_ALT) || Input.IsKeyDown(KeyboardKey.KEY_RIGHT_ALT)) && Input.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            if (Window.IsState(ConfigFlags.FLAG_FULLSCREEN_MODE))
            {
                Window.ToggleFullscreen();
            }
            else
            {
                Window.Maximize();
                Window.ToggleFullscreen();
            }
        }
    }

    protected internal override void Draw()
    { 
        Cam2D.BeginMode2D();
         foreach (var cell in _cellShadows)
             cell.Draw();
        
        foreach (var cell in _cells)
            cell.Draw();
        base.Draw();
        Cam2D.EndMode2D();
        DrawWidgets();
    }
}