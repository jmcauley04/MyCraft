
using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Caches;

public static class ImageCache<T> where T : Enum
{
    /// <summary>
    /// The image cache includes a list of 1 or more bitmaps for each ImageId.
    /// Animation can be achieved by cycling through a list of bitmaps.
    /// </summary>
    private static Dictionary<T, List<Bitmap>> _cache = new Dictionary<T, List<Bitmap>>();

    public static Bitmap CacheImage(T imageId, string path, Vector2 cropPosition, Vector2 cropSize)
    {
        var tmp = Image.FromFile(path);
        var bmp = new Bitmap(tmp);

        var croppedBmp = CropImage(bmp, new Rectangle(cropPosition.ToPoint(), cropSize.ToSize()));

        if (!_cache.ContainsKey(imageId))
            _cache.Add(imageId, new List<Bitmap>());

        _cache[imageId].Add(croppedBmp);
        Log.Info($"Image added to cache: {path}");

        return bmp;
    }

    public static void CacheSpriteSheet(T[,] imageIds, int imgWidth, int imgHeight, string directory)
    {
        for (int c = 0; c < imageIds.GetLength(1); c++)
            for (int r = 0; r < imageIds.GetLength(0); r++)
                CacheImage(imageIds[c, r], directory, new Vector2(r * imgWidth, c * imgHeight), new Vector2(imgWidth, imgHeight));
    }

    public static Bitmap GetImage(T imageId, int seq = 0)
    {
        try
        {
            return _cache[imageId][seq];
        }
        catch (IndexOutOfRangeException)
        {
            Log.Error($"Tried to get image out of range: {imageId.GetType()}, {seq}");
            return _cache[imageId][0];
        }
    }

    private static Bitmap CropImage(Bitmap source, Rectangle section)
    {
        var bitmap = new Bitmap(section.Width, section.Height);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bitmap;
        }
    }
}