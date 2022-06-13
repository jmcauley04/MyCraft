namespace MyGameEngine.Core.Models;

public record Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    static Vector2 _zero = new Vector2(0f, 0f);

    public static Vector2 Zero() => _zero;

    public Vector2() : this(0f, 0f)
    {

    }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2 operator /(Vector2 v1, float d) => new Vector2(v1.X / d, v1.Y / d);
    public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
    public static Vector2 operator *(Vector2 v1, float d) => new Vector2(v1.X * d, v1.Y * d);

    public void Update(float x, float y)
    {
        X = x;
        Y = y;
    }
}
