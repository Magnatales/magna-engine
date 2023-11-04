using Raylib_cs;

namespace Core
{
    public static class Time
    {
        public static float Delta => Raylib.GetFrameTime();
        
        public static double Total => Raylib.GetTime();
        
        public static void Wait(double seconds) => Raylib.WaitTime(seconds);
        
        public static int FrameCount;
    }
}