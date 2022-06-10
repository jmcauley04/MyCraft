using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class Vector2Extensions
{
    public static Point ToPoint(this Vector2 v2) => new((int)v2.X, (int)v2.Y);

    public static Size ToSize(this Vector2 v2) => new((int)v2.X, (int)v2.Y);

    public static float DistanceFrom(this Vector2 v1, Vector2 v2)
    {
        return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
    }

    public static float DirectionFrom(this Vector2 v1, Vector2 v2)
    {
        return (float)Math.Atan2(v1.Y - v2.Y, v1.X - v2.X);
    }

    public static void BoundDistanceFrom(this Vector2 v1, Vector2 v2, float maxDistance)
    {
        if (v1.DistanceFrom(v2) > maxDistance)
        {
            var angle = v1.DirectionFrom(v2);
            var dy = Math.Sin(angle) * maxDistance;
            var dx = Math.Cos(angle) * maxDistance;

            v1.X = (float)(v2.X + dx);
            v1.Y = (float)(v2.Y + dy);
        }
    }
}
