namespace MyGameEngine.Core.Models;

public class Vector2
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
}
