using MyGameEngine.Core.Factories;
using MyGameEngine.Core.Models;

namespace MyCraft2D.GameObjects;

public static class PlayerObject
{
    public static Player Build(float x, float y)
    {
        return GameObjectFactory.RegisterSprite<Player, ImageId>(
            new Vector2(x, y),
            new Vector2(Settings.CharacterSize, Settings.CharacterSize),
            Tags.Player,
            ImageId.PlayerRight);
    }
}
