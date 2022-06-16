using MyGameEngine.Core.Caches;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Factories;

public static class GameObjectFactory
{
    public static T RegisterShape<T>(Vector2 position, Vector2 scale, string tag, int layer = 0)
        where T : Shape2D, new()
    {
        return RegisterDrawable<T>(position, scale, tag, layer);
    }

    public static T RegisterSprite<T, I>(Vector2 position, Vector2 scale, string tag, I imageid, int layer = 0)
        where T : Sprite2D, new()
        where I : Enum
    {
        var sprite = RegisterDrawable<T>(position, scale, tag, layer);
        sprite.Sprite = ImageCache<I>.GetImage(imageid);
        return sprite;
    }

    private static T RegisterDrawable<T>(Vector2 position, Vector2 scale, string tag, int layer = 0)
        where T : GameObject, new()
    {
        var drawable = new T
        {
            Position = position,
            Scale = scale,
            Tag = tag
        };

        GameObjectManager.RegisterGameObject(drawable, layer);

        return drawable;
    }
}
