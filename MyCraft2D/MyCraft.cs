using MyCraft2D.Controllers;
using MyGameEngine.Core;
using MyGameEngine.Core.Assets;
using MyGameEngine.Core.Factories;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyCraft2D;

public class MyCraft : Engine
{
    static int _tileSize = 50;
    static int _charSize = 40;

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
                    var shape = GameObjectFactory.RegisterShape<Shape2D>(new Vector2(y * _tileSize, x * _tileSize), new Vector2(_tileSize, _tileSize), "Ground");
                    shape.Fill = Color.DarkGray;
                }
                else if (map[x, y] == "p")
                {
                    var _player = GameObjectFactory.RegisterSprite<Player, ImageId>(new Vector2(y * _tileSize + _tileSize / 8, x * _tileSize), new Vector2(_charSize, _charSize), "Player", ImageId.PlayerRight);
                    LoadController<PlayerController>()
                        .SetPlayer(_player);

                    LoadController<StatusBarsController>()
                        .SetPlayer(_player);

                    CameraManager.RegisterCamera(new Camera("main")
                    {
                        Target = _player
                    });
                }
            }
        }
    }
}
