using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Controllers;

public abstract class GameController : IGameController
{
    public abstract void OnUpdate();

    public virtual void SetKey(string key, bool target)
    {

    }

    public virtual void SetMouse(string button, bool target, Vector2 coords)
    {

    }
}
