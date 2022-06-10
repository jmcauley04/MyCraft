using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class Vector2Extensions
{
    public static Point ToPoint(this Vector2 v2) => new((int)v2.X, (int)v2.Y);

    public static Size ToSize(this Vector2 v2) => new((int)v2.X, (int)v2.Y);
}
