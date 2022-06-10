using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Controllers;

public abstract class PlayerController : IGameController
{
    protected Sprite2D? _player;

    public abstract void OnUpdate();

    public void SetGameObject(GameObject gameObject)
    {
        _player = gameObject as Sprite2D;
    }

    public abstract void SetKey(string key, bool target);
}
