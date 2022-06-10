
using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Caches;

public static class ImageCache
{
    public enum ImageId
    {
        PlayerDown,
        PlayerRight,
        PlayerLeft,
        PlayerUp,
        //Ground_001
    }

    /// <summary>
    /// The image cache includes a list of 1 or more bitmaps for each ImageId.
    /// Animation can be achieved by cycling through a list of bitmaps.
    /// </summary>
    private static Dictionary<ImageId, List<Bitmap>> _cache = new Dictionary<ImageId, List<Bitmap>>();

    public static Bitmap LoadImage(ImageId imageId, string path)
    {
        var tmp = Image.FromFile(path);
        var bmp = new Bitmap(tmp);

        if (!_cache.ContainsKey(imageId))
            _cache.Add(imageId, new List<Bitmap>());

        _cache[imageId].Add(bmp);
        Log.Info($"Image added to cache: {path}");

        return bmp;
    }

    public static Bitmap LoadImage(ImageId imageId, string path, Vector2 cropPosition, Vector2 cropSize)
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

    public static void CacheImages()
    {
        CachePlayerBitmap(new ImageId[]
        {
            ImageId.PlayerDown, ImageId.PlayerDown, ImageId.PlayerDown, ImageId.PlayerDown,
            ImageId.PlayerRight, ImageId.PlayerRight, ImageId.PlayerRight, ImageId.PlayerRight,
            ImageId.PlayerLeft, ImageId.PlayerLeft, ImageId.PlayerLeft, ImageId.PlayerLeft,
            ImageId.PlayerUp, ImageId.PlayerUp, ImageId.PlayerUp, ImageId.PlayerUp,
        },
        new int[] { 0, 460, 920, 1380 },
        new int[] { 0, 600, 1200, 1800 },
        460,
        590);

        //LoadImage(ImageId.Ground_001, "Assets\\ness_sprite_sheet.png");
    }

    private static void CachePlayerBitmap(ImageId[] ids, int[] rows, int[] cols, int scaleX, int scaleY)
    {
        if (ids.Length < rows.Length * cols.Length)
            throw new Exception("Uh oh! this method requires more ids!");

        int idIndex = 0;

        foreach (var c in cols)
        {
            foreach (var r in rows)
            {
                LoadImage(ids[idIndex++], "Assets\\ness_sprite_sheet.png", new Vector2(r, c), new Vector2(scaleX, scaleY));
            }
        }
    }

    public static Bitmap GetImage(ImageId imageId, int seq = 0) => _cache[imageId][seq];

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