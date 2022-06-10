using MyGameEngine.Core;
using MyGameEngine.Core.Assets;
using MyGameEngine.Core.Controllers;
using MyGameEngine.Core.Factories;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyCraft2D;

public class MyCraft : Engine
{
    ControllersManager _controllersManager;

    static int _tileSize = 50;

    public MyCraft() : base(new Vector2(1200, 800), "My Craft")
    {
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        GetKeys(e, true);
    }

    public override void GetKeyUp(KeyEventArgs e)
    {
        GetKeys(e, false);
    }

    private void GetKeys(KeyEventArgs e, bool target)
    {
        _controllersManager.SetKey(e.KeyCode.ToString(), target);
    }

    public override void OnDraw()
    {
    }

    public override void OnLoad()
    {
        _controllersManager = new();
        MyCraftImageHandler.CacheImages();
        LoadMap(Maps.One);
    }

    public override void OnUpdate()
    {
        _controllersManager.Update();
    }

    public void LoadMap(string[,] map)
    {
        CameraPosition.X = (_screenSize.X - _tileSize * map.GetLength(1)) / 2;
        CameraPosition.Y = (_screenSize.Y - _tileSize * map.GetLength(0)) / 2;

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
                    var _player = GameObjectFactory.RegisterSprite<Sprite2D, ImageId>(new Vector2(y * _tileSize + _tileSize / 8, x * _tileSize), new Vector2(_tileSize, _tileSize), "Player", ImageId.PlayerRight);
                    _controllersManager.LoadController<MyCraftPlayerController>(_player);
                }
            }
        }
    }
}
