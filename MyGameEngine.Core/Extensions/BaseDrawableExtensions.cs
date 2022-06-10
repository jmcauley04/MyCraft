using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class BaseDrawableExtensions
{
    public static void DestroySelf(this BaseDrawable drawable)
    {
        Engine.UnregisterShape(drawable);
    }

    public static Rectangle Rectangle(this Shape2D drawable)
    {
        return new Rectangle((int)drawable.Position.X, (int)drawable.Position.Y, (int)drawable.Scale.X, (int)drawable.Scale.Y);
    }

    public static BaseDrawable? IsColliding(this BaseDrawable drawable, string tag)
    {
        foreach (var gameObject in Engine.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingX(gameObject) && drawable.IsCollidingY(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }

    public static BaseDrawable? IsCollidingX(this BaseDrawable drawable, string tag)
    {
        foreach (var gameObject in Engine.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingX(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }

    public static BaseDrawable? IsCollidingY(this BaseDrawable drawable, string tag)
    {
        foreach (var gameObject in Engine.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingY(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }
}
