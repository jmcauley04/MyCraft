namespace MyGameEngine.Core.Models;

public abstract class GameObject
{
    public Vector2 Position;
    public Vector2 Scale;
    public string Tag = string.Empty;

    public bool IsColliding(Vector2 coord)
    {
        return (Position.X < coord.X &&
            Position.X + Scale.X > coord.X &&
            Position.Y < coord.Y &&
            Position.Y + Scale.Y > coord.Y);
    }

    public bool IsColliding(GameObject? other)
    {
        if (other is null)
            return false;

        if (Position.X < other.Position.X + other.Scale.X &&
            Position.X + Scale.X > other.Position.X &&
            Position.Y < other.Position.Y + other.Scale.Y &&
            Position.Y + Scale.Y > other.Position.Y)
        {
            return true;
        }

        return false;
    }

    public bool IsCollidingX(GameObject? other)
    {
        if (other is null)
            return false;

        if (Position.X < other.Position.X + other.Scale.X &&
            Position.X + Scale.X > other.Position.X)
        {
            return true;
        }

        return false;
    }

    public bool IsCollidingY(GameObject? other)
    {
        if (other is null)
            return false;

        if (Position.Y < other.Position.Y + other.Scale.Y &&
            Position.Y + Scale.Y > other.Position.Y)
        {
            return true;
        }

        return false;
    }

    public abstract void Draw(Graphics g);
}
