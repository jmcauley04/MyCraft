using MyGameEngine.Core.Caches;

namespace MyCraft2D;

public enum ImageId
{
    PlayerDown,
    PlayerRight,
    PlayerLeft,
    PlayerUp,
    //Ground_001
}

public static class MyCraftImageHandler
{
    public static void CacheImages()
    {
        ImageCache<ImageId>.CacheSpriteSheet(new ImageId[,]
        {
            { ImageId.PlayerDown, ImageId.PlayerDown, ImageId.PlayerDown, ImageId.PlayerDown },
            { ImageId.PlayerRight, ImageId.PlayerRight, ImageId.PlayerRight, ImageId.PlayerRight },
            { ImageId.PlayerLeft, ImageId.PlayerLeft, ImageId.PlayerLeft, ImageId.PlayerLeft },
            { ImageId.PlayerUp, ImageId.PlayerUp, ImageId.PlayerUp, ImageId.PlayerUp },
        },
        460,
        600,
        "Assets\\ness_sprite_sheet.png");
    }

    public static Bitmap GetImage(ImageId imageId, int seq = 0) => ImageCache<ImageId>.GetImage(imageId, seq);
}
