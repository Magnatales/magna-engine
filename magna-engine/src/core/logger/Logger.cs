using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Helpers;
using Raylib_cs;

namespace Log;

public unsafe class Logger
{
    private static List<LogEntry>? _logs;
    private static Vector2 _position;
    private const float TIME_TO_DISPLAY = 8f;
    private const float FADE_OUT_TIME = 6f;
    private static string _previousLog = string.Empty;

    public static class Colors
    {
        public static Color Default = new(255, 255, 255, 200);
        public static Color Success = new(100, 255, 100, 200);
        public static Color Error = new(255, 100, 100, 200);
    }
    
    public static void Init(Vector2 position)
    {
        _logs = new List<LogEntry>();
        _position = position;
    }

    public static void ScreenLog(string text, Color? color = null)
    {
        if(_previousLog == text)
            return;

        _previousLog = text;
        _logs?.Insert(0, new LogEntry("> " + text, TIME_TO_DISPLAY, FADE_OUT_TIME, color));
    }

    public static string? LogPath { get; private set; }
    
    public static void Debug(string msg, int skipFrames = 2)
    {
        Log(msg, skipFrames, ConsoleColor.Gray);
    }
    
    public static void Info(string msg, int skipFrames = 2)
    {
        Log(msg, skipFrames, ConsoleColor.Cyan);
    }
    
    public static void Warn(string msg, int skipFrames = 2)
    {
        Log(msg, skipFrames, ConsoleColor.Yellow);
    }
    
    public static void Error(string msg, int skipFrames = 2)
    {
        Log(msg, skipFrames, ConsoleColor.DarkRed);
    }
    
    public static void Fatal(string msg, Exception? exception = null, int skipFrames = 1)
    {
        Log(msg, skipFrames, ConsoleColor.Red);
        throw exception ?? new Exception(msg);
    }
    
    private static void Log(string msg, int skipFrames, ConsoleColor color)
    {
        var stackFrame = new StackFrame(skipFrames, true);
        var method = stackFrame.GetMethod();
        var line = stackFrame.GetFileLineNumber();

        var interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 3);
        interpolatedStringHandler.AppendLiteral("[");
        interpolatedStringHandler.AppendFormatted(method.DeclaringType.FullName);
        interpolatedStringHandler.AppendLiteral(" :: ");
        interpolatedStringHandler.AppendFormatted(method.Name);
        interpolatedStringHandler.AppendLiteral(" (Line: ");
        interpolatedStringHandler.AppendFormatted(line);
        interpolatedStringHandler.AppendLiteral(")] ");
        interpolatedStringHandler.AppendFormatted(msg);
        
        var stringAndClear = interpolatedStringHandler.ToStringAndClear();

        if (LogPath != null)
            FileManager.WriteLine(stringAndClear, LogPath);

        Console.ForegroundColor = color;
        Console.WriteLine(stringAndClear);
        Console.ResetColor();
    }
    
    internal static void CreateLogFile(string directory, int maxLogFiles = 4)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        var logFiles = Directory.GetFiles(directory, "log - *.txt")
            .OrderBy(File.GetLastWriteTime)
            .ToList();

        while (logFiles.Count >= maxLogFiles)
        {
            File.Delete(logFiles[0]);
            logFiles.RemoveAt(0);
        }
        
        var logFileName = $"log - {DateTime.Now:yyyy-MM-dd--HH-mm-ss}.txt";
        LogPath = Path.Combine(directory, logFileName);
        File.Create(LogPath).Close();
    }

    internal static void SetupRaylibLogger()
    {
        Raylib.SetTraceLogCallback(&RaylibLogger);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static void RaylibLogger(int logLevel, sbyte* text, sbyte* args)
    {
        var logMessage = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));
        switch (logLevel)
        {
            case 2:
                Debug(logMessage, 3);
                break;
            case 3:
                Info(logMessage, 3);
                break;
            case 4:
                Warn(logMessage, 3);
                break;
            case 5:
                Error(logMessage, 3);
                break;
            case 6:
                Fatal(logMessage, skipFrames: 3);
                break;
        }
    }

    public static void Draw()
    {
        var yOffset = 0;
        for (var index = 0; index < _logs?.Count; index++)
        {
            var log = _logs[index];

            if (log.TimeToDisplay <= 0)
            {
                log.UpdateFadeOut(Raylib.GetFrameTime());
                if (log.FadeOutTime <= 2f)
                {
                    _logs.RemoveAt(index);
                    index--;
                    continue;
                }
            }
            else
            {
                log.UpdateTime(Raylib.GetFrameTime());
            }

            var alpha = (int)(255 * (log.FadeOutTime / FADE_OUT_TIME));
            Raylib.DrawRectangleRounded(
                new Rectangle((int)_position.X - 10, (int)_position.Y + yOffset - 2.5f,
                    100 + Raylib.MeasureText(log.Text, Fonts.Default.BaseSize),
                    Fonts.Default.BaseSize + 5),
                0.5f,
                0,
                log.Color
            );
            Raylib.DrawTextEx(Fonts.Default, log.Text,
                new Vector2((int)_position.X, (int)_position.Y + yOffset), Fonts.Default.BaseSize, 4,
                new Color(0, 0, 0, alpha));
            yOffset += 30;
        }
    }
}