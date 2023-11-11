using System.Numerics;
using Actors;
using Core;
using Game.UI;
using Motion;
using Raylib_cs;

namespace HelloWorld;

public class TestShaderScene : Scene
{
    public TestShaderScene(string name) : base(name)
    {
    }

    private Shader _outlineShader;

    public override void Init()
    {
        base.Init();
         var random = new Random();
         var greenOutlineMaterial = new OutlineMaterial(Textures.GetScarfy(), new[] { 0.25f, 1f, 0.25f, 1.0f }, 1.5f);
         var blueOutlineMaterial = new OutlineMaterial(Textures.GetScarfy(), new[] { 0.25f, 0.25f, 1f, 1.0f }, 1.5f);
         var redOutlineMaterial = new OutlineMaterial(Textures.GetScarfy(), new[] { 1f, 0.25f, 0.25f, 1.0f }, 1.5f);
        
         for (var i = 0; i < 40000; i++)
         {
             var randomX = random.Next(-4400, Raylib.GetScreenWidth() + 4400);
             var randomY = random.Next(-4800, Raylib.GetScreenHeight() + 4800);
        
             var emptyActor = new EmptyActor(new Vector2(randomX, randomY));
             emptyActor.AddComponent(Sprites.ScarfyAuto(emptyActor));
             emptyActor.TryGetComponent<Sprite2D>(out var emptyActorSprite);
             var randomValue = Raylib.GetRandomValue(0, 2);
             switch (randomValue)
             {
                 case 0:
                     emptyActorSprite?.SetMaterial(greenOutlineMaterial);
                     break;
                 case 1:
                     emptyActorSprite?.SetMaterial(redOutlineMaterial);
                     break;
                 case 2:
                     emptyActorSprite?.SetMaterial(blueOutlineMaterial);
                     break;
             }
             AddActorAndOverrideId(emptyActor, emptyActor.Id + " " + i);
         }
        
         var scarfy = new Scarfy(new Vector2(350f, 280f));
         scarfy.TryGetComponent<Sprite2D>(out var sprite2D);
         sprite2D?.SetMaterial(redOutlineMaterial);
         scarfy.Layer = 1;
         AddActor(scarfy);
         Cam2D.SetTarget(scarfy);
         AddWidget(new GamePanel());
    }

    protected internal override void Draw()
    {
        Cam2D.BeginMode2D();
        base.Draw();
        Cam2D.EndMode2D();
        DrawWidgets();
    }
}