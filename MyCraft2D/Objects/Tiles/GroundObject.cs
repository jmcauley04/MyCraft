using MyGameEngine.Core.Factories;
using MyGameEngine.Core.Models;

namespace MyCraft2D.GameObjects.Tiles;

public static class GroundObject
{
    public static Shape2D Build(float x, float y, int layer = 0)
    {
        var shape = GameObjectFactory.RegisterShape<Shape2D>(
            new Vector2(x, y),
            new Vector2(Settings.TileSize,
            Settings.TileSize),
            Tags.Ground,
            layer);

        shape.Fill = Color.DarkGray;
        return shape;
    }
}
