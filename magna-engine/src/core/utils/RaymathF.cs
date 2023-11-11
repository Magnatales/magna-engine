
using System.Numerics;
using Raylib_cs;

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
    
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        var direction = target - current;
        var distance = direction.Length();

        if (distance <= maxDistanceDelta ||
            distance == 0) return target; // We've reached the target or the target is at the same position

        return current + direction / distance * maxDistanceDelta;
    }
    
    static Random random = new Random();

        /// <summary>
        /// Returns a random integer
        /// </summary>
        /// <returns>An int between 0 and infinity</returns>
        public static int Next() => random.Next();

        /// <summary>
        /// Returns a random integer between 0 and <paramref name="maxValue"/> 
        /// </summary>
        /// <param name="maxValue">The maximum value to return</param>
        /// <returns>A number between 0 and <paramref name="maxValue"/></returns>
        public static int Next(int maxValue) => random.Next(maxValue);

        /// <summary>
        /// Returns a random integer within the specified range
        /// </summary>
        /// <param name="minValue">The minimum integer to return</param>
        /// <param name="maxValue">The maximum integer to return</param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue) => random.Next(minValue, maxValue);

        /// <summary>
        /// Returns a random 32-bit float
        /// </summary>
        /// <returns>A number between 0.0f and 1.0f</returns>
        public static float NextFloat()
        {
            return (float)random.NextDouble();
        }

        /// <summary>
        /// Returns a random number within the specified range
        /// </summary> 
        /// <param name="minValue">The minimum end of the range</param>
        /// <param name="maxValue">The maximum end of the range</param>
        /// <returns>A radom float between <paramref name="minValue"/> and <paramref name="maxValue"/></returns>
        public static float NextFloat(float minValue, float maxValue)
        {
            return minValue + (float)random.NextDouble() * (maxValue - minValue);
        }

        /// <summary>
        /// Returns a unit vector in a random direction
        /// </summary>
        /// <returns>A random unit Vector2</returns>
        public static Vector2 NextDirection()
        {
            float angle = NextFloat(0, MathF.PI * 2);
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        /// <summary>
        /// Returns a unit vector in a random direction between two angles
        /// </summary>
        /// <param name="minAngle">The minimum angle for the random direction</param>
        /// <param name="maxAngle">The maximum angle for the random direction</param>
        /// <returns>A random unit Vector2</returns>
        public static Vector2 RandomDirection(float minAngle, float maxAngle)
        {
            float angle = NextFloat(minAngle, maxAngle);
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        /// <summary>
        /// Returns a Vector2 that falls within the supplied <paramref name="bounds"/>
        /// </summary>
        /// <param name="bounds">A rectangle defining the bounds which should contain the point</param>
        /// <returns>A Vector2 wihtin the bounds</returns>
        public static Vector2 RandomPosition(Rectangle bounds)
        {
            return new Vector2(
                NextFloat(bounds.X, bounds.X + bounds.Width),
                NextFloat(bounds.Y, bounds.Y + bounds.Height)
                );
        }
        
        public static float ToRadians(float degrees)
        {
            return degrees * (float)(Math.PI / 180.0);
        }
}