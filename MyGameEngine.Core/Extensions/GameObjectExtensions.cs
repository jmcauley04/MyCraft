using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class GameObjectExtensions
{
    public static void DestroySelf(this GameObject drawable)
    {
        GameObjectManager.UnregisterGameObject(drawable);
    }

    public static Rectangle Rectangle(this Shape2D drawable)
    {
        return new Rectangle((int)drawable.Position.X, (int)drawable.Position.Y, (int)drawable.Scale.X, (int)drawable.Scale.Y);
    }

    public static GameObject? IsColliding(this GameObject drawable, string tag)
    {
        foreach (var gameObject in GameObjectManager.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingX(gameObject) && drawable.IsCollidingY(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }

    public static GameObject? IsCollidingX(this GameObject drawable, string tag)
    {
        foreach (var gameObject in GameObjectManager.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingX(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }

    public static GameObject? IsCollidingY(this GameObject drawable, string tag)
    {
        foreach (var gameObject in GameObjectManager.GetGameObjects())
        {
            if (gameObject.Tag == tag && drawable.IsCollidingY(gameObject))
            {
                return gameObject;
            }
        }

        return null;
    }
}
