using MyGameEngine.Core.Interfaces;

namespace MyGameEngine.Core.Controllers;

public abstract class GameController : IGameController
{
    public abstract void OnUpdate();

    public abstract void SetKey(string key, bool target);
}
