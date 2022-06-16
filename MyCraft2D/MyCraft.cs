using MyCraft2D.Controllers;
using MyCraft2D.GameObjects;
using MyCraft2D.GameObjects.Tiles;
using MyGameEngine.Core;
using MyGameEngine.Core.Assets;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyCraft2D;

public class MyCraft : Engine
{
    Player? Player;

    public MyCraft() : base(new Vector2(1200, 800), "MyCraft")
    {
    }

    public override void OnDraw()
    {
        if (Player is not null)
            DrawLayer = Player.Layer;
    }

    public override void OnLoad()
    {
        MyCraftImageHandler.CacheImages();
        LoadMap(Maps.One);
    }

    public void LoadMap(string[,,] map)
    {
        CameraManager.SetScreensize(_screenSize);

        for (int h = 0; h < map.GetLength(0); h++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(2); y++)
                {
                    if (map[h, x, y] == "g")
                    {
                        GroundObject.Build(y * Settings.TileSize, x * Settings.TileSize, h - 2);
                    }
                    else if (map[h, x, y] == "p")
                    {
                        Player = PlayerObject.Build(y * Settings.TileSize + Settings.TileSize / 8, x * Settings.TileSize);
                        LoadController<PlayerController>()
                            .SetPlayer(Player);

                        LoadController<StatusBarsController>()
                            .SetPlayer(Player);

                        CameraManager.RegisterCamera(new Camera(Tags.Camera_Main)
                        {
                            Target = Player
                        });
                    }
                }
            }
        }
    }
}
