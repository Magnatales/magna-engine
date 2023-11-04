using System.Numerics;
using Entities;
using Log;
using Raylib_cs;

namespace Components;

public class PositionLogger : Component
{
    private readonly Actor _owner;
    private string _id;
    private bool _useScreenLog = false;

    public PositionLogger(Actor owner, string id, bool useScreenLog = false)
    {
        _id = id;
        _owner = owner;
        _useScreenLog = useScreenLog;
    }
    
    public override void DrawDebug()
    {
        var text = $"{_id} at {_owner}";
        if(_useScreenLog)
            Logger.ScreenLog(text);
        else
        {
            Raylib.DrawTextEx(Fonts.Default,  $"{_id} at {_owner}", _owner.Position + new Vector2(-135, 70), Fonts.Default.BaseSize, 2, Color.BLACK);
        }
    }

}