namespace MyGameEngine.Shared.Models;

public abstract class BaseDrawable
{
    public Vector2 Position;
    public Vector2 Scale;
    public string Tag = string.Empty;

    public bool IsColliding(BaseDrawable? other)
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

    public bool IsCollidingX(BaseDrawable? other)
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

    public bool IsCollidingY(BaseDrawable? other)
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
}
