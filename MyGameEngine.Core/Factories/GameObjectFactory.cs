using MyGameEngine.Core.Caches;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Factories;

public static class GameObjectFactory
{
    public static T RegisterShape<T>(Vector2 position, Vector2 scale, string tag) where T : Shape2D, new()
    {
        return RegisterDrawable<T>(position, scale, tag);
    }

    public static T RegisterSprite<T>(Vector2 position, Vector2 scale, string tag, ImageCache.ImageId imageid) where T : Sprite2D, new()
    {
        var sprite = RegisterDrawable<T>(position, scale, tag);
        sprite.Sprite = ImageCache.GetImage(imageid);
        return sprite;
    }

    private static T RegisterDrawable<T>(Vector2 position, Vector2 scale, string tag) where T : BaseDrawable, new()
    {
        var drawable = new T
        {
            Position = position,
            Scale = scale,
            Tag = tag
        };

        Engine.RegisterShape(drawable);
        Log.Message($"{tag} has been registered", ConsoleColor.Green, typeof(T).Name);

        return drawable;
    }
}
