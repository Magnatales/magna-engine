using Raylib_cs;

namespace Log;

public class LogEntry
{
    public string Text { get; }
    private float _timeToDisplay;
    private float _fadeOutTime;
    public Color Color;

    public LogEntry(string text, float timeToDisplay, float fadeOutTime, Color? color = null)
    {
        Text = text;
        _timeToDisplay = timeToDisplay;
        _fadeOutTime = fadeOutTime;
        Color = color ?? Logger.Colors.Default;
    }

    public float TimeToDisplay => _timeToDisplay;

    public float FadeOutTime => _fadeOutTime;

    public void UpdateTime(float deltaTime)
    {
        _timeToDisplay -= deltaTime;
    }
    
    public void UpdateFadeOut(float deltaTime)
    {
        _fadeOutTime -= deltaTime;
    }
}