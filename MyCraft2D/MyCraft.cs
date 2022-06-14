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
    public MyCraft() : base(new Vector2(1200, 800), "MyCraft")
    {
    }

    public override void OnDraw()
    {
    }

    public override void OnLoad()
    {
        MyCraftImageHandler.CacheImages();
        LoadMap(Maps.One);
    }

    public void LoadMap(string[,] map)
    {
        CameraManager.SetScreensize(_screenSize);

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == "g")
                {
                    GroundObject.Build(y * Settings.TileSize, x * Settings.TileSize);
                }
                else if (map[x, y] == "p")
                {
                    var _player = PlayerObject.Build(y * Settings.TileSize + Settings.TileSize / 8, x * Settings.TileSize);
                    LoadController<PlayerController>()
                        .SetPlayer(_player);

                    LoadController<StatusBarsController>()
                        .SetPlayer(_player);

                    CameraManager.RegisterCamera(new Camera(Tags.Camera_Main)
                    {
                        Target = _player
                    });
                }
            }
        }
    }
}
