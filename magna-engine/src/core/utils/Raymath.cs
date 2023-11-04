
public static class RaymathF
{
    public static int FloorToInt(float value)
    {
        return (int)MathF.Floor(value);
    }
    
    public static bool RandValue()
    {
        return new Random().Next(0, 2) == 1;
    }
}